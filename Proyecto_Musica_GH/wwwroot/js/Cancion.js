(() => {
    const Cancion = {
        tabla: null,

        init() {
            this.inicializarTabla();
            this.registrarEventos();
            this.cargarAlbums();
        },

        inicializarTabla() {
            this.tabla = $('#tblCancion').DataTable({
                ajax: {
                    url: '/Cancion/ObtenerCanciones',
                    type: 'GET',
                    dataSrc: 'data'
                },
                columns: [
                    { data: 'cancion_ID' },
                    { data: 'titulo' },
                    {
                        data: null,
                        render: function (data, type, row) {
                            if (!row.artista_ID) return row.artistaNombre || '-';
                            return `<a href="/Artista/Detalle/${row.artista_ID}">${row.artistaNombre || 'Ver artista'}</a>`;
                        }
                    },
                    { data: 'duracion' },
                    { data: 'fecha_publicacion' },
                    {
                        data: null,
                        render: function (data, type, row) {
                            if (!row.album_ID) return row.albumNombre || '-';
                            return `<a href="/Album/Detalle/${row.album_ID}">${row.albumNombre || 'Ver álbum'}</a>`;
                        }
                    },

                    // BOTÓN PLAY GLOBAL
                    {
                        data: null,
                        render: function (data, type, row) {
                            return `<button class="btn btn-success btn-play" data-id="${row.cancion_ID}">
                                        ▶ Play
                                    </button>`;
                        }
                    },

                   
                    {
                        data: null,
                        orderable: false,
                        render: function (data, type, row) {
                            return `
                                <button class="btn btn-primary btn-editar" data-id="${row.cancion_ID}">
                                    Editar
                                </button>
                                <button class="btn btn-danger btn-eliminar" data-id="${row.cancion_ID}">
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
            $('#tblCancion').on('click', '.btn-editar', function () {
                const id = $(this).data('id');
                Cancion.cargarDatosCancion(id);
            });

            // ELIMINAR
            $('#tblCancion').on('click', '.btn-eliminar', function () {
                const id = $(this).data('id');
                Cancion.eliminarCancion(id);
            });

            // GUARDAR
            $('#btnGuardarCancion').on('click', function () {
                Cancion.guardarCancion();
            });

            // EDITAR GUARDAR
            $('#btnEditarCancion').on('click', function () {
                Cancion.editarCancion();
            });

            $('#AlbumSelect').on('change', function () {
                const artista = $(this).find('option:selected').data('artista');
                $('#ArtistaPreview').val(artista || 'Seleccione un álbum');
            });
        },

        guardarCancion() {
            let form = $('#formCrearCancion')[0];
            let formData = new FormData(form);
           
            formData.set("Album_ID", $('#AlbumSelect').val());

            $.ajax({
                url: $(form).attr('action'),
                type: 'POST',
                data: formData,
                processData: false,
                contentType: false,
                success: function (response) {
                    if (response.esCorrecto) {
                        $('#modalCrearCancion').modal('hide');
                        form.reset();
                        Cancion.tabla.ajax.reload();

                        Swal.fire('Éxito', response.mensaje, 'success');
                    } else {
                        Swal.fire('Error', response.mensaje, 'warning');
                    }
                }
            });
        },

        cargarDatosCancion(id) {
            $.get(`/Cancion/ObtenerCancionPorId?id=${id}`, function (result) {
                if (result.esCorrecto) {
                    let data = result.data;

                    $('#CancionId').val(data.cancion_ID);
                    $('#Titulo').val(data.titulo);
                    $('#Duracion').val(data.duracion);
                    $('#Fecha_publicacion').val(data.fecha_publicacion);
                    $('#Album_ID').val(data.album_ID);
                    $('#URL_cancion').val(data.url_cancion);

                    $('#modalEditarCancion').modal('show');
                }
            });
        },

        editarCancion() {
            let form = $('#formEditarCancion');

            if (!form.valid()) return;

            $.ajax({
                url: form.attr('action'),
                type: 'POST',
                data: form.serialize(),
                success: function (response) {
                    if (response.esCorrecto) {
                        $('#modalEditarCancion').modal('hide');
                        form[0].reset();

                        Cancion.tabla.ajax.reload();

                        Swal.fire('Éxito', response.mensaje, 'success');
                    } else {
                        Swal.fire('Error', response.mensaje, 'warning');
                    }
                }
            });
        },

        eliminarCancion(id) {
            Swal.fire({
                title: '¿Eliminar canción?',
                text: "No podrás revertir esto",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonText: 'Sí, eliminar'
            }).then((result) => {
                if (result.isConfirmed) {

                    $.post('/Cancion/EliminarCancion', { id: id }, (response) => {
                        if (response.esCorrecto) {
                            Cancion.tabla.ajax.reload();

                            Swal.fire('Eliminado', response.mensaje, 'success');
                        }
                    });
                }
            });
        },
        cargarAlbums() {
            $.get('/Album/ObtenerAlbums', function (result) {
                if (result.esCorrecto) {
                    let select = $('#AlbumSelect');
                    select.empty();
                    select.append('<option value="">-- Seleccione un álbum --</option>');
                    result.data.forEach(album => {
                        select.append(`<option value="${album.album_ID}" data-artista="${album.artistaNombre || 'Sin artista'}">${album.titulo}</option>`);
                    });
                }
            });
        }
    };

    $(document).ready(() => Cancion.init());
})();
