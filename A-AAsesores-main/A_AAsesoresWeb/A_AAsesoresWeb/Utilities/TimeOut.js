var sessionTimeoutMinutes = 1; // Tiempo de inactividad en minutos
var logoutTimeoutSeconds = 10; // Tiempo para cerrar sesión después de la advertencia en segundos
var lastActivityTime;
var logoutTimer;
var countdownTimer;

// Función para actualizar la marca de tiempo de la última actividad
function updateLastActivityTime() {
    lastActivityTime = new Date();
}

// Función para mostrar la ventana emergente de extensión de sesión
function showLogoutConfirmation() {
    var timeRemaining = logoutTimeoutSeconds;

    // Utiliza SweetAlert2 para mostrar una ventana emergente con el temporizador
    Swal.fire({
        title: '¿Extender Sesión?',
        html: `La sesión se cerrará en <span id="countdown">${timeRemaining}</span> segundos debido a la inactividad.<br>`,
        icon: 'warning',
        confirmButtonText: 'Extender Sesión',
        allowOutsideClick: false,
        allowEscapeKey: false,
        allowEnterKey: false,
        showCancelButton: false,
        showCloseButton: false,
        showConfirmButton: true
    });

    // Actualizar el contador cada segundo
    countdownTimer = setInterval(function () {
        timeRemaining--;
        document.getElementById('countdown').innerText = timeRemaining;

        // Cerrar automáticamente la ventana después del tiempo de espera si no hay respuesta
        if (timeRemaining <= 0) {
            clearInterval(countdownTimer);
            Swal.close();
            // Redirigir a la página de inicio de sesión después de cerrar la sesión
            window.location.href = '/Employee/LoginEmployee';
        }
    }, 1000);
}

// Función para cerrar la sesión automáticamente después de un período de inactividad
function autoLogout() {
    // Mostrar ventana emergente antes de cerrar la sesión
    showLogoutConfirmation();
}

// Función para verificar la inactividad del usuario y cerrar sesión si es necesario
function checkSessionTimeout() {
    var currentTime = new Date();
    var timeDiff = (currentTime - lastActivityTime) / 1000; // Diferencia de tiempo en segundos
    var minutesDiff = Math.floor(timeDiff / 60); // Diferencia de tiempo en minutos

    if (minutesDiff >= sessionTimeoutMinutes) {
        // Iniciar temporizador para cerrar la sesión automáticamente después de un período de advertencia
        logoutTimer = setTimeout(autoLogout, logoutTimeoutSeconds * 1000);
    } else {
        // Reiniciar el temporizador de cierre de sesión si el usuario responde antes del tiempo de advertencia
        clearInterval(countdownTimer);
        Swal.close();
        clearTimeout(logoutTimer);
    }
}

// Eventos para registrar la actividad del usuario
$(document).on('click mousemove keydown', function () {
    updateLastActivityTime(); // Actualizar la marca de tiempo de la última actividad cuando el usuario interactúa con la página
    clearInterval(countdownTimer); // Reiniciar el temporizador de cuenta regresiva
    clearTimeout(logoutTimer); // Reiniciar el temporizador de cierre de sesión
});

// Ejecutar la función de verificación de tiempo de espera cada minuto
setInterval(checkSessionTimeout, 60000); // 60000 milisegundos = 1 minuto
