(() => {
    const Artista = {
        tabla: null,

        init() {
            this.inicializarTabla();
            this.registrarEventos();
        },

        inicializarTabla() {
            this.tabla = $('#tblArtista').DataTable({
                ajax: {
                    url: '/Artista/ObtenerArtistas',
                    type: 'GET',
                    dataSrc: 'data'
                },
                columns: [
                    { data: 'artista_ID' },
                    { data: 'nombre' },
                    {
                        data: 'albumes',
                        render: function (data) {
                            return data && data.length ? data.length : 0;
                        }
                    },
                    {
                        data: null,
                        orderable: false,
                        render: function (data, type, row) {
                            return `<a class="btn btn-sm btn-outline-secondary" href="/Artista/Detalle/${row.artista_ID}">Ver detalle</a>`;
                        }
                    },
                    {
                        data: null,
                        orderable: false,
                        render: function (data, type, row) {
                            return `
                                <button class="btn btn-sm btn-primary btn-editar" data-id="${row.artista_ID}">Editar</button>
                                <button class="btn btn-sm btn-danger btn-eliminar" data-id="${row.artista_ID}">Eliminar</button>
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
            $('#btnGuardarArtista').on('click', () => this.guardarArtista());
            $('#btnEditarArtista').on('click', () => this.editarArtista());

            $('#tblArtista').on('click', '.btn-editar', (e) => {
                this.cargarArtista($(e.currentTarget).data('id'));
            });

            $('#tblArtista').on('click', '.btn-eliminar', (e) => {
                this.eliminarArtista($(e.currentTarget).data('id'));
            });
        },

        guardarArtista() {
            const form = $('#formCrearArtista');
            $.post(form.attr('action'), form.serialize(), (response) => {
                if (response.esCorrecto) {
                    $('#modalCrearArtista').modal('hide');
                    form[0].reset();
                    this.tabla.ajax.reload();
                    Swal.fire('Éxito', response.mensaje, 'success');
                } else {
                    Swal.fire('Error', response.mensaje, 'warning');
                }
            });
        },

        cargarArtista(id) {
            $.get(`/Artista/ObtenerArtistaPorId?id=${id}`, (response) => {
                if (response.esCorrecto) {
                    const data = response.data;
                    $('#ArtistaId').val(data.artista_ID);
                    $('#ArtistaNombre').val(data.nombre);
                    $('#ArtistaBiografia').val(data.biografia);
                    $('#modalEditarArtista').modal('show');
                }
            });
        },

        editarArtista() {
            const form = $('#formEditarArtista');
            $.post(form.attr('action'), form.serialize(), (response) => {
                if (response.esCorrecto) {
                    $('#modalEditarArtista').modal('hide');
                    form[0].reset();
                    this.tabla.ajax.reload();
                    Swal.fire('Éxito', response.mensaje, 'success');
                } else {
                    Swal.fire('Error', response.mensaje, 'warning');
                }
            });
        },

        eliminarArtista(id) {
            Swal.fire({
                title: '¿Eliminar artista?',
                text: 'Si tiene álbumes asociados, no se podrá eliminar.',
                icon: 'warning',
                showCancelButton: true,
                confirmButtonText: 'Sí, eliminar'
            }).then((result) => {
                if (!result.isConfirmed) return;

                $.post('/Artista/EliminarArtista', { id: id }, (response) => {
                    if (response.esCorrecto) {
                        this.tabla.ajax.reload();
                        Swal.fire('Éxito', response.mensaje, 'success');
                    } else {
                        Swal.fire('Error', response.mensaje, 'warning');
                    }
                });
            });
        }
    };

    $(document).ready(() => Artista.init());
})();
