(() => {
    const Cancion = {
        tabla: null,
        audio: null,

        init() {
            this.audio = document.getElementById("audioPlayer");

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
                        render: function (data, type, row) {
                            return `<button class="btn btn-success btn-play" data-id="${row.cancion_ID}">
                                        ▶ Play
                                    </button>`;
                        }
                    },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return `
                                <button class="btn btn-primary btn-editar" data-id="${row.cancion_ID}">Editar</button>
                                <button class="btn btn-danger btn-eliminar" data-id="${row.cancion_ID}">Eliminar</button>
                            `;
                        }
                    }
                ]
            });
        },

        registrarEventos() {

            $('#tblCancion').on('click', '.btn-play', (e) => {
                let id = $(e.currentTarget).data('id');

                $.post('/Reproductor/SeleccionarCancion', { id: id }, (res) => {
                    if (!res.esCorrecto) return;

                    let c = res.data;

                    this.audio.src = c.url_cancion;
                    this.audio.play();

                    $('#tituloActual').text(c.titulo);
                });
            });

            $('#btnNext').on('click', () => {
                $.post('/Reproductor/Siguiente', (res) => {
                    if (!res.esCorrecto) return;

                    let c = res.data;

                    this.audio.src = c.url_cancion;
                    this.audio.play();

                    $('#tituloActual').text(c.titulo);
                });
            });

            $('#btnPrev').on('click', () => {
                $.post('/Reproductor/Previa', (res) => {
                    if (!res.esCorrecto) return;

                    let c = res.data;

                    this.audio.src = c.url_cancion;
                    this.audio.play();

                    $('#tituloActual').text(c.titulo);
                });
            });

            $('#btnPlay').on('click', () => {
                this.audio.play();
            });

            $('#btnStop').on('click', () => {
                this.audio.pause();
                this.audio.currentTime = 0;
            });

            this.audio.addEventListener('ended', () => {
                $('#btnNext').click();
            });
        }
    };

    $(document).ready(() => Cancion.init());
})();