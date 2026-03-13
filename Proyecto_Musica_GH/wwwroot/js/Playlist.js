(() => {
    const Playlist = {
        tabla: null,

        init() {
            this.inicializarTabla();
            this.registrarEventos();
        },

        inicializarTabla() {
            this.tabla = $('#tblPlaylist').DataTable({
                ajax: {
                    url: '/Playlist/ObtenerPlaylists',
                    type: 'GET',
                    dataSrc: 'data'
                },
                columns: [
                    { data: 'playlist_ID' },
                    { data: 'nombre' },
                    { data: 'fecha_creacion' },
                    { data: 'usuarioNombre' },
                    {
                        data: null,
                        title: 'Acciones',
                        orderable: false,
                        render: function (data, type, row) {
                            return `
                                <button class="btn btn-sm btn-primary btn-editar" data-id="${row.playlist_ID}">
                                    <i class="bi bi-pencil"></i> Editar
                                </button>
                                <button class="btn btn-sm btn-danger btn-eliminar" data-id="${row.playlist_ID}">
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
            $('#tblPlaylist').on('click', '.btn-editar', function () {
                const id = $(this).data('id');
                Playlist.cargarDatosPlaylist(id);
            });

            $('#tblPlaylist').on('click', '.btn-eliminar', function () {
                const id = $(this).data('id');
                Playlist.eliminarPlaylist(id);
            });

            $('#btnGuardarPlaylist').on('click', function () {
                Playlist.guardarPlaylist();
            });

            $('#btnEditarPlaylist').on('click', function () {
                Playlist.editarPlaylist();
            });
        },

        guardarPlaylist() {
            let form = $('#formCrearPlaylist');
            if (!form.valid()) return;

            $.ajax({
                url: form.attr('action'),
                type: 'POST',
                data: form.serialize(),
                success: function (response) {
                    if (response.esCorrecto) {
                        $('#modalCrearPlaylist').modal('hide');
                        form[0].reset();
                        Playlist.tabla.ajax.reload();
                        Swal.fire('Éxito', response.mensaje, 'success');
                    } else {
                        Swal.fire('Error', response.mensaje, 'warning');
                    }
                }
            });
        },

        cargarDatosPlaylist(id) {
            $.get(`/Playlist/ObtenerPlaylistPorId?id=${id}`, function (result) {
                if (result.esCorrecto) {
                    let data = result.data;
                    $('#PlaylistId').val(data.playlist_ID);
                    $('#Nombre').val(data.nombre);
                    $('#Fecha_creacion').val(data.fecha_creacion);
                    $('#Usuario_ID').val(data.usuario_ID);
                    $('#modalEditarPlaylist').modal('show');
                }
            });
        },

        editarPlaylist() {
            let form = $('#formEditarPlaylist');
            if (!form.valid()) return;

            $.ajax({
                url: form.attr('action'),
                type: 'POST',
                data: form.serialize(),
                success: function (response) {
                    if (response.esCorrecto) {
                        $('#modalEditarPlaylist').modal('hide');
                        form[0].reset();
                        Playlist.tabla.ajax.reload();
                        Swal.fire('Éxito', response.mensaje, 'success');
                    } else {
                        Swal.fire('Error', response.mensaje, 'warning');
                    }
                }
            });
        },

        eliminarPlaylist(id) {
            Swal.fire({
                title: '¿Estás seguro?',
                text: "¡No podrás revertir esta operación!",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonText: 'Sí, eliminar',
            }).then((result) => {
                if (result.isConfirmed) {
                    $.ajax({
                        url: '/Playlist/EliminarPlaylist',
                        type: 'POST',
                        data: { id: id },
                        success: function (response) {
                            if (response.esCorrecto) {
                                Playlist.tabla.ajax.reload();
                                Swal.fire('Éxito', response.mensaje, 'success');
                            }
                        }
                    });
                }
            });
        }
    };

    $(document).ready(() => Playlist.init());
})();