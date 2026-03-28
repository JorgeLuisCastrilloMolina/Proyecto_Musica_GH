(() => {
    const Album = {
        tabla: null,

        init() {
            this.inicializarTabla();
            this.registrarEventos();
        },

        inicializarTabla() {
            this.tabla = $('#tblAlbum').DataTable({
                ajax: {
                    url: '/Album/ObtenerAlbums',
                    type: 'GET',
                    dataSrc: 'data'
                },
                columns: [
                    { data: 'album_ID' },
                    { data: 'titulo' },
                    { data: 'fecha_publicacion' },

                    // ⚙️ ACCIONES
                    {
                        data: null,
                        orderable: false,
                        render: function (data, type, row) {
                            return `
                                <button class="btn btn-primary btn-editar" data-id="${row.album_ID}">
                                    Editar
                                </button>
                                <button class="btn btn-danger btn-eliminar" data-id="${row.album_ID}">
                                    Eliminar
                                </button>
                            `;
                        }
                    }
                ],
                language: {
                    url: 'https://cdn.datatables.net/plug-ins/1.13.6/i18n/es-ES.json'
                }
            });
        },

        registrarEventos() {
            // EDITAR
            $('#tblAlbum').on('click', '.btn-editar', function () {
                const id = $(this).data('id');
                Album.cargarDatosAlbum(id);
            });

            // ELIMINAR
            $('#tblAlbum').on('click', '.btn-eliminar', function () {
                const id = $(this).data('id');
                Album.eliminarAlbum(id);
            });

            // GUARDAR
            $('#btnGuardarAlbum').on('click', function () {
                Album.guardarAlbum();
            });

            // EDITAR GUARDAR
            $('#btnEditarAlbum').on('click', function () {
                Album.editarAlbum();
            });
        },

        guardarAlbum() {
            let form = $('#formCrearAlbum')[0];
            let formData = $(form).serialize();

            $.ajax({
                url: $(form).attr('action'),
                type: 'POST',
                data: formData,
                success: function (response) {
                    if (response.esCorrecto) {
                        $('#modalCrearAlbum').modal('hide');
                        form.reset();
                        Album.tabla.ajax.reload();

                        Swal.fire('Éxito', response.mensaje, 'success');
                    } else {
                        Swal.fire('Error', response.mensaje, 'warning');
                    }
                }
            });
        },

        cargarDatosAlbum(id) {
            $.get(`/Album/ObtenerAlbumPorId?id=${id}`, function (result) {
                if (result.esCorrecto) {
                    let data = result.data;

                    $('#AlbumId').val(data.album_ID);
                    $('#Titulo').val(data.titulo);
                    $('#Fecha_publicacion').val(data.fecha_publicacion);

                    $('#modalEditarAlbum').modal('show');
                }
            });
        },

        editarAlbum() {
            let form = $('#formEditarAlbum');

            if (!form.valid()) return;

            $.ajax({
                url: form.attr('action'),
                type: 'POST',
                data: form.serialize(),
                success: function (response) {
                    if (response.esCorrecto) {
                        $('#modalEditarAlbum').modal('hide');
                        form[0].reset();

                        Album.tabla.ajax.reload();

                        Swal.fire('Éxito', response.mensaje, 'success');
                    } else {
                        Swal.fire('Error', response.mensaje, 'warning');
                    }
                }
            });
        },

        eliminarAlbum(id) {
            Swal.fire({
                title: '¿Eliminar álbum?',
                text: "No podrás revertir esto",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonText: 'Sí, eliminar'
            }).then((result) => {
                if (result.isConfirmed) {
                    $.post('/Album/EliminarAlbum', { id: id }, (response) => {
                        if (response.esCorrecto) {
                            Album.tabla.ajax.reload();
                            Swal.fire('Eliminado', response.mensaje, 'success');
                        }
                    });
                }
            });
        }
    };

    $(document).ready(() => Album.init());
})();