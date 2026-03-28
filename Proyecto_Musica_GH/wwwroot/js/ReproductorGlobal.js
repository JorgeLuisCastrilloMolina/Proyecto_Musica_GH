(() => {
    const Player = {
        audio: null,

        init() {
            this.audio = document.getElementById("audioPlayer");

            if (!this.audio) return;

            this.cargarEstado(); 
            this.registrarEventos();
        },

        registrarEventos() {

            // PLAY DESDE TABLA
            $(document).on('click', '.btn-play', (e) => {
                let id = $(e.currentTarget).data('id');

                $.post('/Reproductor/SeleccionarCancion', { id: id }, (res) => {

                    if (!res.esCorrecto) return;

                    let c = res.data;

                    this.audio.src = c.url_cancion;
                    this.audio.play();

                    $('#tituloActual').text("🎵 " + c.titulo);

                    // GUARDAR ESTADO
                    localStorage.setItem("cancion", JSON.stringify(c));
                    localStorage.setItem("tiempo", 0);
                    localStorage.setItem("reproduciendo", true);
                });
            });

            // SIGUIENTE
            $('#btnNext').on('click', () => {
                $.post('/Reproductor/Siguiente', (res) => {
                    if (!res.esCorrecto) return;

                    let c = res.data;

                    this.audio.src = c.url_cancion;
                    this.audio.play();

                    $('#tituloActual').text("🎵 " + c.titulo);

                    localStorage.setItem("cancion", JSON.stringify(c));
                    localStorage.setItem("tiempo", 0);
                    localStorage.setItem("reproduciendo", true);
                });
            });

            //  ANTERIOR
            $('#btnPrev').on('click', () => {
                $.post('/Reproductor/Previa', (res) => {
                    if (!res.esCorrecto) return;

                    let c = res.data;

                    this.audio.src = c.url_cancion;
                    this.audio.play();

                    $('#tituloActual').text("🎵 " + c.titulo);

                    localStorage.setItem("cancion", JSON.stringify(c));
                    localStorage.setItem("tiempo", 0);
                    localStorage.setItem("reproduciendo", true);
                });
            });

            //  PLAY
            $('#btnPlay').on('click', () => {
                this.audio.play();
                localStorage.setItem("reproduciendo", true);
            });

            //  STOP
            $('#btnStop').on('click', () => {
                this.audio.pause();
                this.audio.currentTime = 0;

                localStorage.setItem("reproduciendo", false);
            });

            //  GUARDAR TIEMPO CONSTANTEMENTE
            this.audio.addEventListener('timeupdate', () => {
                localStorage.setItem("tiempo", this.audio.currentTime);
            });

            // AUTO SIGUIENTE
            this.audio.addEventListener('ended', () => {
                $('#btnNext').click();
            });
        },

        //  RESTAURAR ESTADO
        cargarEstado() {

            let cancion = localStorage.getItem("cancion");
            let tiempo = localStorage.getItem("tiempo");
            let reproduciendo = localStorage.getItem("reproduciendo");

            if (!cancion) return;

            let c = JSON.parse(cancion);

            this.audio.src = c.url_cancion;
            $('#tituloActual').text("🎵 " + c.titulo);

            this.audio.currentTime = tiempo || 0;

            if (reproduciendo === "true") {
                this.audio.play().catch(() => { });
            }
        }
    };

    $(document).ready(() => Player.init());
})();