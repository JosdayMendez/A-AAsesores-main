function getBotResponse(input) {
    if (input == "Hola") {
        return "¡Hola! ¿En qué puedo ayudarte?";
    } else if (input == "adios") {
        return "¡Hasta luego!";
    } else if (input == "Cómo estás?") {
        return "Estoy bien, ¡gracias por preguntar!";
    } else if (input == "Cuál es tu nombre?") {
        return "Mi nombre es ChatBot.";
    } else {
        return "Lo siento, no entiendo. ¿Puedes ser más específico?";
    }
}
