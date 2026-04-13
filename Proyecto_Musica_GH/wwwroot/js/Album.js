(() => {
    const Album = {
        tabla: null,

        init() {
            this.inicializarTabla();
            this.registrarEventos();
            this.cargarArtistas();
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
                    {
                        data: 'artistaNombre',
                        defaultContent: '',
                        render: function (data) {
                            return data || 'Sin artista';
                        }
                    },
                    {
                        data: 'fecha_publicacion',
                        defaultContent: ''
                    },
                    {
                        data: null,
                        orderable: false,
                        render: function (data, type, row) {
                            return `<a class="btn btn-sm btn-outline-secondary" href="/Album/Detalle/${row.album_ID}">Ver canciones</a>`;
                        }
                    },
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

            // EDITAR 
            $('#btnEditarAlbum').on('click', function () {
                Album.editarAlbum();
            });
        },

        guardarAlbum() {
            let form = $('#formCrearAlbum');
            let formData = form.serializeArray();
            formData = formData.filter(x => x.name !== 'Artista_ID');
            formData.push({ name: 'Artista_ID', value: $('#ArtistaSelect').val() });

            $.ajax({
                url: form.attr('action'),
                type: 'POST',
                data: formData,
                success: function (response) {
                    if (response.esCorrecto) {
                        $('#modalCrearAlbum').modal('hide');
                        form[0].reset();
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
                    // Buscar el álbum dentro del array
                    let data = Array.isArray(result.data)
                        ? result.data.find(a => a.album_ID == id)
                        : result.data;

                    if (!data) {
                        console.warn("No se encontró el álbum con id:", id);
                        return;
                    }

                    console.log("Titulo recibido:", data.titulo);
                    console.log("Fecha recibida:", data.fecha_publicacion);

                    $('#AlbumId').val(data.album_ID);
                    $('#TituloEditar').val(data.titulo || '');
                    $('#Fecha_publicacionEditar').val(data.fecha_publicacion || '');
                    $('#Artista_ID').val(data.artista_ID);

                    $('#modalEditarAlbum').modal('show');
                }
            });
        },


        editarAlbum() {
            let form = $('#formEditarAlbum');

            if (!form.valid()) return;

            let formData = form.serializeArray();
            formData = formData.filter(x => x.name !== 'Artista_ID');
            formData.push({ name: 'Artista_ID', value: $('#Artista_ID').val() });

            $.ajax({
                url: form.attr('action'),
                type: 'POST',
                data: formData,
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
        },
        cargarArtistas() {
            $.get('/Artista/ObtenerArtistas', function (result) {
                if (!result.esCorrecto) return;

                const selectCrear = $('#ArtistaSelect');
                const selectEditar = $('#Artista_ID');

                selectCrear.empty().append('<option value="">-- Seleccione un artista --</option>');
                selectEditar.empty().append('<option value="">-- Seleccione un artista --</option>');

                result.data.forEach(artista => {
                    const option = `<option value="${artista.artista_ID}">${artista.nombre}</option>`;
                    selectCrear.append(option);
                    selectEditar.append(option);
                });
            });
        }
    };

    $(document).ready(() => Album.init());
})();
