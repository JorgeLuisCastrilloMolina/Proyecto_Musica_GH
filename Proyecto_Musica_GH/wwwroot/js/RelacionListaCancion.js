(() => {
    const Relacion = {
        tabla: null,

        init() {
            this.inicializarTabla();
            this.registrarEventos();
        },

        inicializarTabla() {
            this.tabla = $('#tblRelacion').DataTable({
                ajax: {
                    url: '/RelacionListaCancion/ObtenerCancionesPorPlaylist?playlistId=1', // ejemplo, puedes cambiar dinámicamente
                    type: 'GET',
                    dataSrc: 'data'
                },
                columns: [
                    { data: 'lc_REL_ID' },
                    { data: 'playlistNombre' },
                    { data: 'cancionTitulo' },
                    { data: 'orden' },
                    {
                        data: null,
                        title: 'Acciones',
                        orderable: false,
                        render: function (data, type, row) {
                            return `
                                <button class="btn btn-sm btn-danger btn-eliminar" data-id="${row.lc_REL_ID}">
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
            $('#btnGuardarRelacion').on('click', function () {
                Relacion.guardarRelacion();
            });

            $('#btnEditarRelacion').on('click', function () {
                Relacion.editarRelacion();
            });

            $('#tblRelacion').on('click', '.btn-eliminar', function () {
                const id = $(this).data('id');
                Relacion.eliminarRelacion(id);
            });
        },

        guardarRelacion() {
            let form = $('#formCrearRelacion');
            if (!form.valid()) return;

            $.ajax({
                url: form.attr('action'),
                type: 'POST',
                data: form.serialize(),
                success: function (response) {
                    if (response.esCorrecto) {
                        $('#modalCrearRelacion').modal('hide');
                        form[0].reset();
                        Relacion.tabla.ajax.reload();
                        Swal.fire('Éxito', response.mensaje, 'success');
                    } else {
                        Swal.fire('Error', response.mensaje, 'warning');
                    }
                }
            });
        },

        editarRelacion() {
            let form = $('#formEditarRelacion');
            if (!form.valid()) return;

            $.ajax({
                url: form.attr('action'),
                type: 'POST',
                data: form.serialize(),
                success: function (response) {
                    if (response.esCorrecto) {
                        $('#modalEditarRelacion').modal('hide');
                        form[0].reset();
                        Relacion.tabla.ajax.reload();
                        Swal.fire('Éxito', response.mensaje, 'success');
                    } else {
                        Swal.fire('Error', response.mensaje, 'warning');
                    }
                }
            });
        },

        eliminarRelacion(id) {
            Swal.fire({
                title: '¿Estás seguro?',
                text: "¡No podrás revertir esta operación!",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonText: 'Sí, eliminar',
            }).then((result) => {
                if (result.isConfirmed) {
                    $.ajax({
                        url: '/RelacionListaCancion/EliminarCancionDePlaylist',
                        type: 'POST',
                        data: { lc_rel_id: id },
                        success: function (response) {
                            if (response.esCorrecto) {
                                Relacion.tabla.ajax.reload();
                                Swal.fire('Éxito', response.mensaje, 'success');
                            }
                        }
                    });
                }
            });
        }
    };

    $(document).ready(() => Relacion.init());
})();