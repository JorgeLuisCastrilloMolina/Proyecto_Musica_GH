(() => {
    const Cancion = {
        tabla: null,

        init() {
            this.inicializarTabla();
            this.registrarEventos();
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
                    { data: 'duracion' },
                    { data: 'fecha_publicacion' },
                    { data: 'albumNombre' },
                    {
                        data: null,
                        title: 'Acciones',
                        orderable: false,
                        render: function (data, type, row) {
                            return `
                                <button class="btn btn-sm btn-primary btn-editar" data-id="${row.cancion_ID}">
                                    <i class="bi bi-pencil"></i> Editar
                                </button>
                                <button class="btn btn-sm btn-danger btn-eliminar" data-id="${row.cancion_ID}">
                                    <i class="bi bi-trash"></i> Eliminar
                                </button>`;
                        }
                    }
                ],
                language: {
                    url: 'https://cdn.datatables.net/plug-ins/1.13.6/i18n/es-ES.json'
                }
            });
        },

        registrarEventos() {
            $('#tblCancion').on('click', '.btn-editar', function () {
                const id = $(this).data('id');
                Cancion.cargarDatosCancion(id);
            });

            $('#tblCancion').on('click', '.btn-eliminar', function () {
                const id = $(this).data('id');
                Cancion.eliminarCancion(id);
            });

            $('#btnGuardarCancion').on('click', function () {
                Cancion.guardarCancion();
            });

            $('#btnEditarCancion').on('click', function () {
                Cancion.editarCancion();
            });
        },

        guardarCancion() {
            let form = $('#formCrearCancion');
            if (!form.valid()) return;

            $.ajax({
                url: form.attr('action'),
                type: 'POST',
                data: form.serialize(),
                success: function (response) {
                    if (response.esCorrecto) {
                        $('#modalCrearCancion').modal('hide');
                        form[0].reset();
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
                title: '¿Estás seguro?',
                text: "¡No podrás revertir esta operación!",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonText: 'Sí, eliminar',
            }).then((result) => {
                if (result.isConfirmed) {
                    $.ajax({
                        url: '/Cancion/EliminarCancion',
                        type: 'POST',
                        data: { id: id },
                        success: function (response) {
                            if (response.esCorrecto) {
                                Cancion.tabla.ajax.reload();
                                Swal.fire('Éxito', response.mensaje, 'success');
                            }
                        }
                    });
                }
            });
        }
    };

    $(document).ready(() => Cancion.init());
})();