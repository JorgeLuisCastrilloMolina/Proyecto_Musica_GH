(() => {
    const Player = {
        audio: null,

        init() {
            this.audio = document.getElementById("audioPlayer");

            if (!this.audio) {
                console.error("Audio no encontrado");
                return;
            }

            this.registrarEventos();
        },

        registrarEventos() {

            // ▶ PLAY DESDE CUALQUIER TABLA
            $(document).on('click', '.btn-play', (e) => {
                let id = $(e.currentTarget).data('id');

                $.post('/Reproductor/SeleccionarCancion', { id: id }, (res) => {

                    if (!res.esCorrecto) {
                        alert(res.mensaje);
                        return;
                    }

                    let c = res.data;

                    if (!c.url_cancion) {
                        alert("No hay audio");
                        return;
                    }

                    this.audio.src = c.url_cancion;
                    this.audio.play();

                    $('#tituloActual').text("🎵 " + c.titulo);
                });
            });

            // ⏭ SIGUIENTE
            $('#btnNext').on('click', () => {
                $.post('/Reproductor/Siguiente', (res) => {
                    if (!res.esCorrecto) return;

                    let c = res.data;

                    this.audio.src = c.url_cancion;
                    this.audio.play();

                    $('#tituloActual').text("🎵 " + c.titulo);
                });
            });

            // ⏮ ANTERIOR
            $('#btnPrev').on('click', () => {
                $.post('/Reproductor/Previa', (res) => {
                    if (!res.esCorrecto) return;

                    let c = res.data;

                    this.audio.src = c.url_cancion;
                    this.audio.play();

                    $('#tituloActual').text("🎵 " + c.titulo);
                });
            });

            // ▶ PLAY
            $('#btnPlay').on('click', () => {
                this.audio.play();
            });

            // ⏹ STOP
            $('#btnStop').on('click', () => {
                this.audio.pause();
                this.audio.currentTime = 0;
            });

            // 🔁 AUTO SIGUIENTE
            this.audio.addEventListener('ended', () => {
                $('#btnNext').click();
            });
        }
    };

    $(document).ready(() => Player.init());
})();