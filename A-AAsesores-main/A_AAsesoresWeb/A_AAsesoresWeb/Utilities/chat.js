//// Collapsible
//var coll = document.getElementsByClassName("collapsible");

//for (let i = 0; i < coll.length; i++) {
//    coll[i].addEventListener("click", function () {
//        this.classList.toggle("active");

//        var content = this.nextElementSibling;

//        if (content.style.maxHeight) {
//            content.style.maxHeight = null;
//        } else {
//            content.style.maxHeight = content.scrollHeight + "px";
//        }

//    });
//}

//function getTime() {
//    let today = new Date();
//    hours = today.getHours();
//    minutes = today.getMinutes();

//    if (hours < 10) {
//        hours = "0" + hours;
//    }

//    if (minutes < 10) {
//        minutes = "0" + minutes;
//    }

//    let time = hours + ":" + minutes;
//    return time;
//}

//// Obtiene el primer mensaje
//function firstBotMessage() {
//    let firstMessage = "¡Hola! ¿Cómo podemos ayudarte?"
//    document.getElementById("botStarterMessage").innerHTML = '<p class="botText"><span>' + firstMessage + '</span></p>';

//    let time = getTime();

//    $("#chat-timestamp").append(time);
//    document.getElementById("userInput").scrollIntoView(false);
//}

//firstBotMessage();

//// Obtiene la respuesta del bot
//function getHardResponse(userText) {
//    let botResponse = getBotResponse(userText);
//    let botHtml = '<p class="botText"><span>' + botResponse + '</span></p>';
//    $("#chatbox").append(botHtml);

//    document.getElementById("chat-bar-bottom").scrollIntoView(true);
//}

//// Obtiene el texto del cuadro de entrada y lo procesa
//function getResponse() {
//    let userText = $("#textInput").val();

//    if (userText.trim() === "") {
//        // Si el usuario envía un mensaje vacío, muestra el menú de preguntas frecuentes
//        showFaqMenu();
//        return;
//    }

//    let userHtml = '<p class="userText"><span>' + userText + '</span></p>';

//    $("#textInput").val("");
//    $("#chatbox").append(userHtml);
//    document.getElementById("chat-bar-bottom").scrollIntoView(true);

//    setTimeout(() => {
//        getHardResponse(userText);
//    }, 1000);
//}


//// Muestra el menú de preguntas frecuentes como burbujas de texto
//function showFaqMenu() {
//    let faqMenu = "Aquí tienes algunas preguntas frecuentes:";
//    let botHtml = '<p class="botText"><span>' + faqMenu + '</span></p>';
//    $("#chatbox").append(botHtml);

//    // Agregar burbujas de texto para cada pregunta frecuente
//    let preguntasFrecuentes = [
//        "¿Cómo puedo registrarme?",
//        "¿Cuál es el horario de atención?",
//        "¿Cómo puedo restablecer mi contraseña?",
//        "¿Cuáles son los métodos de pago aceptados?"
//    ];

//    preguntasFrecuentes.forEach(function (pregunta, index) {
//        let bubbleHtml = '<div class="bubble">' +
//            '<button class="faqButton" onclick="handleFaqButtonClick(' + (index + 1) + ')">' + pregunta + '</button>' +
//            '</div>';
//        $("#chatbox").append(bubbleHtml);
//    });

//    // Desplazarse al final del chatbox
//    document.getElementById("chat-bar-bottom").scrollIntoView(true);
//}


//// Manejar clics en botones de preguntas frecuentes
//function handleFaqButtonClick(numeroPregunta) {
//    // Realizar la acción correspondiente a la pregunta seleccionada
//    switch (numeroPregunta) {
//        case 1:
//            // Acción para la pregunta 1
//            console.log("Acción para la pregunta 1: ¿Cómo puedo registrarme?");
//            break;
//        case 2:
//            // Acción para la pregunta 2
//            console.log("Acción para la pregunta 2: ¿Cuál es el horario de atención?");
//            break;
//        case 3:
//            // Acción para la pregunta 3
//            console.log("Acción para la pregunta 3: ¿Cómo puedo restablecer mi contraseña?");
//            break;
//        case 4:
//            // Acción para la pregunta 4
//            console.log("Acción para la pregunta 4: ¿Cuáles son los métodos de pago aceptados?");
//            break;
//        default:
//            // Acción por defecto (si no coincide con ninguna pregunta)
//            console.log("Acción por defecto");
//            break;
//    }
//}

//// Maneja el envío de texto mediante clics en botones
//function buttonSendText(sampleText) {
//    let userHtml = '<p class="userText"><span>' + sampleText + '</span></p>';

//    $("#textInput").val("");
//    $("#chatbox").append(userHtml);
//    document.getElementById("chat-bar-bottom").scrollIntoView(true);

//}
 
//function sendButton() {
//    getResponse();
//}

//function heartButton() {
//    buttonSendText("¡Clic en corazón!")
//}

//// Presiona enter para enviar un mensaje
//$("#textInput").keypress(function (e) {
//    if (e.which == 13) {
//        getResponse();
//    }
//});
