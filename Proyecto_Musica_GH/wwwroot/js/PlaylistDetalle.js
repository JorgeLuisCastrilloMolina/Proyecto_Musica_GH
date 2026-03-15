(() => {
    const form = document.getElementById('formAgregarCancionPlaylist');

    if (form) {
        form.addEventListener('submit', async (event) => {
            event.preventDefault();

            const payload = new URLSearchParams(new FormData(form));

            const response = await fetch('/RelacionListaCancion/AgregarCancionAPlaylist', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8'
                },
                body: payload
            });

            const result = await response.json();

            if (result.esCorrecto) {
                window.location.reload();
                return;
            }

            Swal.fire('Error', result.mensaje, 'warning');
        });
    }

    document.querySelectorAll('.btn-eliminar-relacion').forEach((button) => {
        button.addEventListener('click', () => {
            const relacionId = button.dataset.id;

            Swal.fire({
                title: '¿Quitar canción?',
                text: 'La canción se eliminará de esta playlist.',
                icon: 'warning',
                showCancelButton: true,
                confirmButtonText: 'Sí, quitar'
            }).then(async (result) => {
                if (!result.isConfirmed) {
                    return;
                }

                const response = await fetch('/RelacionListaCancion/EliminarCancionDePlaylist', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8'
                    },
                    body: new URLSearchParams({ lc_rel_id: relacionId })
                });

                const data = await response.json();

                if (data.esCorrecto) {
                    window.location.reload();
                    return;
                }

                Swal.fire('Error', data.mensaje, 'warning');
            });
        });
    });
})();
