$(function () {
    document.getElementById("tab1").style.display = "block";
    document.getElementsByClassName("tablinks")[0].classList.add("active");
    cargarGraficosTab1();
});
function openTab(evt, tabName) {
    var i, tabcontent, tablinks;

    // Ocultar todas las pestañas
    tabcontent = document.getElementsByClassName("tabcontent");
    for (i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = "none";
    }

    // Quitar la clase "active" de todos los enlaces de pestañas
    tablinks = document.getElementsByClassName("tablinks");
    for (i = 0; i < tablinks.length; i++) {
        tablinks[i].classList.remove("active");
    }

    // Agregar la clase "active" al enlace de pestaña actual
    evt.currentTarget.classList.add("active");

    // Mostrar el contenido de la pestaña actual
    var currentTab = document.getElementById(tabName);
    currentTab.style.display = "block";

    // Limpiar todos los canvas dentro del tab activo
    var canvases = currentTab.getElementsByTagName("canvas");
    for (i = 0; i < canvases.length; i++) {
        var canvas = canvases[i];
        var ctx = canvas.getContext('2d');
        ctx.clearRect(0, 0, canvas.width, canvas.height);
    }
    var colors = generateColorList();

    if (tabName === "tab1") {
        // Llamar a las funciones para cargar los gráficos en el tab3
        cargarGraficosTab1(colors);
    } else if (tabName === "tab2") {
        // Llamar a las funciones para cargar los gráficos en otroTab
        cargarGraficosTab2(colors);
    } else if (tabName === "tab3") {
        // Llamar a las funciones para cargar los gráficos en otroTab
        cargarGraficosTab3(colors);
    } else if (tabName === "tab4") {
        // Llamar a las funciones para cargar los gráficos en otroTab
        cargarGraficosTab4(colors);
    }
        
}
function getRandomColor() {
    var letters = '0123456789ABCDEF';
    var color = '#';
    for (var i = 0; i < 6; i++) {
        color += letters[Math.floor(Math.random() * 16)];
    }
    return color; //Retorna el color
}
function generateColorList() {
    // Lista de colores fijos
    const fixedColors = ['#2d3142', '#983628', '#28afb0', '#0b6e4f'];
    // Lista de colores aleatorios
    const randomColors = [];
    // Agregar colores fijos al principio
    randomColors.push(...fixedColors);
    // Generar colores aleatorios para el resto
    for (let i = 0; i < 10 - fixedColors.length; i++) {
        randomColors.push(getRandomColor());
    }
    return randomColors;
}
function cargarGraficosTab1(colors) {
    var colors = generateColorList();
    // Cargar el gráfico de barras para la distribución de propiedades por precio
    loadPropertiesByPrice();
    // Cargar el gráfico de barras para la distribución de propiedades por mes en el año
    loadPropertiesByCreationMonth()
    // Cargar el gráfico de pastel para la distribución de propiedades por estado
    loadPropertiesByStatus(colors);
    // Cargar el gráfico de pastel para propiedades por tipo
    loadPropertyByType(colors);
    // Cargar el gráfico de dona para la distribución de propiedades por provincia
    loadPropertiesByProvince(colors);
    // Cargar el gráfico de dona para la distribución de propiedades por transacción
    loadPropertiesByTransaction(colors);
    // Cargar el gráfico de líneas el monto generado por mes en el año
    loadEarningsByMonth();
}
function cargarGraficosTab2(colors) {
    //Cargar el gráfico de barras de propiedades por empleado
    loadPropertiesByEmployee();
    // Cargar el gráfico de barras de clientes por mes
    loadClientsByMonth();
    // Cargar el gráfico de pastel para de empleados por estado
    loadEmployeesByStatus(colors);
    // Cargar el gráfico de pastel para de empleados por rol
    loadEmployeesByRole(colors);
}
function cargarGraficosTab3(colors) {
    // Cargar el gráfico de barras de clientes por mes
    loadQuotesByMonth();
    // Cargar el gráfico de pastel para empleados por estado
    loadQuotesByStatus(colors);
}
function cargarGraficosTab4(colors) {
    // Cargar el gráfico de barras de citas pendientes por empleado
    loadPendingAppoitmentsByEmployee();
    // Cargar el gráfico de barras de citas por propiedad
    loadAppointmentsByProperty();
    // Cargar el gráfico de barras citas por fecha de creación y atención
    loadAppointmentsByCreationAndAttention();
    // Cargar el gráfico de pastel para citas por estado
    loadAppointmentsByStatus(colors);
}
function loadPropertiesByPrice() {
    $.ajax({
        url: '/Reports/PropertiesByPrice',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            var propertyTitles = [];
            var propertyPrices = [];

            $.each(data, function (index, item) {
                propertyTitles.push(item.Title);
                propertyPrices.push(item.Price);
            });

            var canvas = document.getElementById('barsByPrice');
            var ctx = canvas.getContext('2d');

            // Destruir cualquier gráfico existente en el canvas
            if (canvas.chart) {
                canvas.chart.destroy();
            }

            // Crear el nuevo gráfico
            canvas.chart = new Chart(ctx, {
                type: 'bar',
                data: {
                    labels: propertyTitles,
                    datasets: [{
                        label: 'Monto',
                        data: propertyPrices,
                        backgroundColor: '#0B6E4F',
                        borderColor: '#0B6E4F',
                        borderWidth: 1
                    }]
                },
                options: {
                    responsive: true,
                    maintainAspectRatio: false,
                    plugins: {
                        legend: {
                            position: 'top',
                        },
                        title: {
                            display: true,
                            text: 'Distribución de propiedades según su precio',
                            font: {
                                size: 20
                            }
                        }
                    }
                },
            });

            // Manejar el evento de redimensionamiento de la ventana
            window.addEventListener('resize', function () {
                canvas.chart.resize(); // Vuelve a renderizar el gráfico cuando cambia el tamaño de la ventana
            });
        },
        error: function () {
            console.log('Error al obtener los datos.');
        }
    });
}
function loadPropertiesByCreationMonth() {
    $.ajax({
        url: '/Reports/PropertiesByCreationMonthOnPresentYear',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            var propertyCount = [];
            var propertyMonths = [];
            
            $.each(data, function (index, item) {
                propertyCount.push(item.PropertyCount);
                propertyMonths.push(item.Month);
            });

            var canvas = document.getElementById('barsPropertyByMonthOfYear');
            var ctx = canvas.getContext('2d');

            // Destruir cualquier gráfico existente en el canvas
            if (canvas.chart) {
                canvas.chart.destroy();
            }

            // Crear el nuevo gráfico
            canvas.chart = new Chart(ctx, {
                type: 'bar',
                data: {
                    labels: propertyMonths,
                    datasets: [{
                        label: 'Creadas',
                        data: propertyCount,
                        backgroundColor: '#0B6E4F',
                        borderColor: '#0B6E4F',
                        borderWidth: 1
                    }]
                },
                options: {
                    responsive: true,
                    maintainAspectRatio: false,
                    plugins: {
                        legend: {
                            position: 'top',
                        },
                        title: {
                            display: true,
                            text: 'Cantidad de propiedades según su mes de creación en el presente año',
                            font: {
                                size: 20
                            }
                        }
                    }
                },
            });

            // Manejar el evento de redimensionamiento de la ventana
            window.addEventListener('resize', function () {
                canvas.chart.resize(); // Vuelve a renderizar el gráfico cuando cambia el tamaño de la ventana
            });
        },
        error: function () {
            console.log('Error al obtener los datos.');
        }
    });
}
function loadPropertiesByStatus(colors) {
    $.ajax({
        url: '/Reports/PropertiesByStatus',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            var propertyCount = [];
            var propertyStatuses = [];
            var backgroundColors = [];

            // Usar los colores proporcionados en lugar de generar colores aleatorios
            backgroundColors = colors;

            $.each(data, function (index, item) {
                propertyCount.push(item.PropertyCount);
                propertyStatuses.push(item.Status);
            });

            var canvas = document.getElementById('piePropertyByStatus');
            var ctx = canvas.getContext('2d');

            // Destruir cualquier gráfico existente en el canvas
            if (canvas.chart) {
                canvas.chart.destroy();
            }

            // Crear el nuevo gráfico
            canvas.chart = new Chart(ctx, {
                type: 'pie',
                data: {
                    labels: propertyStatuses,
                    datasets: [{
                        label: 'Cantidad',
                        data: propertyCount,
                        backgroundColor: backgroundColors,
                        borderColor: backgroundColors,
                        borderWidth: 1
                    }]
                },
                options: {
                    responsive: true,
                    maintainAspectRatio: false,
                    plugins: {
                        legend: {
                            position: 'top',
                        },
                        title: {
                            display: true,
                            text: 'Distribución de propiedades según su estado',
                            font: {
                                size: 20
                            }
                        }
                    }
                },
            });

            // Manejar el evento de redimensionamiento de la ventana
            window.addEventListener('resize', function () {
                canvas.chart.resize(); // Vuelve a renderizar el gráfico cuando cambia el tamaño de la ventana
            });
        },
        error: function () {
            console.log('Error al obtener los datos.');
        }
    });
}
function loadPropertyByType(colors) {
    $.ajax({
        url: '/Reports/PropertyByType',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            var propertyCount = [];
            var propertyType = [];
            var backgroundColors = [];

            backgroundColors = colors;

            $.each(data, function (index, item) {
                propertyCount.push(item.PropertyCount);
                propertyType.push(item.Type);
            });

            var canvas = document.getElementById('piePropertyByType');
            var ctx = canvas.getContext('2d');

            // Destruir cualquier gráfico existente en el canvas
            if (canvas.chart) {
                canvas.chart.destroy();
            }

            // Crear el nuevo gráfico
            canvas.chart = new Chart(ctx, {
                type: 'pie',
                data: {
                    labels: propertyType,
                    datasets: [{
                        label: 'Cantidad',
                        data: propertyCount,
                        backgroundColor: backgroundColors,
                        borderColor: backgroundColors,
                        borderWidth: 1
                    }]
                },
                options: {
                    responsive: true,
                    maintainAspectRatio: false,
                    plugins: {
                        legend: {
                            position: 'top',
                        },
                        title: {
                            display: true,
                            text: 'Distribución de propiedades según su tipo',
                            font: {
                                size: 20
                            }
                        }
                    }
                },
            });

            // Manejar el evento de redimensionamiento de la ventana
            window.addEventListener('resize', function () {
                canvas.chart.resize(); // Vuelve a renderizar el gráfico cuando cambia el tamaño de la ventana
            });
        },
        error: function () {
            console.log('Error al obtener los datos.');
        }
    });
}
function loadPropertiesByProvince(colors) {
    $.ajax({
        url: '/Reports/PropertiesByProvince',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            var propertyCount = [];
            var propertyProvinces = [];
            var backgroundColors = [];

            backgroundColors = colors;

            $.each(data, function (index, item) {
                propertyCount.push(item.PropertyCount);
                propertyProvinces.push(item.Province);
            });

            var canvas = document.getElementById('doughnutByProvince');
            var ctx = canvas.getContext('2d');

            // Destruir cualquier gráfico existente en el canvas
            if (canvas.chart) {
                canvas.chart.destroy();
            }

            // Crear el nuevo gráfico
            canvas.chart = new Chart(ctx, {
                type: 'doughnut',
                data: {
                    labels: propertyProvinces,
                    datasets: [{
                        label: 'Cantidad',
                        data: propertyCount,
                        backgroundColor: backgroundColors,
                        borderColor: backgroundColors,
                        borderWidth: 1
                    }]
                },
                options: {
                    responsive: true,
                    maintainAspectRatio: false,
                    plugins: {
                        legend: {
                            position: 'top',
                        },
                        title: {
                            display: true,
                            text: 'Distribución de propiedades por provincia',
                            font: {
                                size: 20
                            }
                        }
                    }
                },
            });

            // Manejar el evento de redimensionamiento de la ventana
            window.addEventListener('resize', function () {
                canvas.chart.resize(); // Vuelve a renderizar el gráfico cuando cambia el tamaño de la ventana
            });
        },
        error: function () {
            console.log('Error al obtener los datos.');
        }
    });
}
function loadPropertiesByTransaction(colors) {
    $.ajax({
        url: '/Reports/PropertiesByTransaction',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            var propertyCount = [];
            var propertyTransactions = [];
            var backgroundColors = [];

            backgroundColors = colors;

            $.each(data, function (index, item) {
                propertyCount.push(item.PropertyCount);
                propertyTransactions.push(item.Transaction);
            });

            var canvas = document.getElementById('doughnutByTransactonType');
            var ctx = canvas.getContext('2d');

            // Destruir cualquier gráfico existente en el canvas
            if (canvas.chart) {
                canvas.chart.destroy();
            }

            // Crear el nuevo gráfico
            canvas.chart = new Chart(ctx, {
                type: 'doughnut',
                data: {
                    labels: propertyTransactions,
                    datasets: [{
                        label: 'Cantidad',
                        data: propertyCount,
                        backgroundColor: backgroundColors,
                        borderColor: backgroundColors,
                        borderWidth: 1
                    }]
                },
                options: {
                    responsive: true,
                    maintainAspectRatio: false,
                    plugins: {
                        legend: {
                            position: 'top',
                        },
                        title: {
                            display: true,
                            text: 'Distribución de propiedades por tipo de transacción',
                            font: {
                                size: 20
                            }
                        }
                    }
                },
            });

            // Manejar el evento de redimensionamiento de la ventana
            window.addEventListener('resize', function () {
                canvas.chart.resize(); // Vuelve a renderizar el gráfico cuando cambia el tamaño de la ventana
            });
        },
        error: function () {
            console.log('Error al obtener los datos.');
        }
    });
}
function loadEarningsByMonth() {
    $.ajax({
        url: '/Reports/EarningsByMonthOnPresentYear',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            var propertyMonths = [];
            var monthEarnings = [];

            $.each(data, function (index, item) {
                propertyMonths.push(item.Month);
                monthEarnings.push(item.MonthEarnings);
            });

            var canvas = document.getElementById('lineEarningsByMonthOnPresentYear');
            var ctx = canvas.getContext('2d');

            // Destruir cualquier gráfico existente en el canvas
            if (canvas.chart) {
                canvas.chart.destroy();
            }

            // Crear el nuevo gráfico
            canvas.chart = new Chart(ctx, {
                type: 'line',
                data: {
                    labels: propertyMonths,
                    datasets: [{
                        label: 'Ganancias',
                        data: monthEarnings,
                        backgroundColor: '#2e3142',
                        borderColor: '#b89531',
                        borderWidth: 1,
                        pointStyle: 'circle',
                        pointRadius: 7.5,
                        pointHoverRadius: 10
                    }]
                },
                options: {
                    responsive: true,
                    maintainAspectRatio: false,
                    plugins: {
                        legend: {
                            position: 'top',
                        },
                        title: {
                            display: true,
                            text: 'Ingresos generados mensualmente en el presente año',
                            font: {
                                size: 20
                            }
                        }
                    },
                },
            });

            // Manejar el evento de redimensionamiento de la ventana
            window.addEventListener('resize', function () {
                canvas.chart.resize(); // Vuelve a renderizar el gráfico cuando cambia el tamaño de la ventana
            });
        },
        error: function () {
            console.log('Error al obtener los datos.');
        }
    });
}
function loadPropertiesByEmployee() {
    $.ajax({
        url: '/Reports/PropertiesByEmployee',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            var propertyEmployees = [];
            var propertyCount = [];
            
            $.each(data, function (index, item) {
                propertyEmployees.push(item.Employee);
                propertyCount.push(item.PropertyCount);
            });

            var canvas = document.getElementById('barsPropertiesByEmployee');
            var ctx = canvas.getContext('2d');

            // Destruir cualquier gráfico existente en el canvas
            if (canvas.chart) {
                canvas.chart.destroy();
            }

            // Crear el nuevo gráfico
            canvas.chart = new Chart(ctx, {
                type: 'bar',
                data: {
                    labels: propertyEmployees,
                    datasets: [{
                        label: 'Cantidad de propiedades',
                        data: propertyCount,
                        backgroundColor: '#0B6E4F',
                        borderColor: '#0B6E4F',
                        borderWidth: 1
                    }]
                },
                options: {
                    responsive: true,
                    maintainAspectRatio: false,
                    plugins: {
                        legend: {
                            position: 'top',
                        },
                        title: {
                            display: true,
                            text: 'Cantidad de propiedades asignadas por empleado',
                            font: {
                                size: 20
                            }
                        }
                    }
                },
            });

            // Manejar el evento de redimensionamiento de la ventana
            window.addEventListener('resize', function () {
                canvas.chart.resize(); // Vuelve a renderizar el gráfico cuando cambia el tamaño de la ventana
            });
        },
        error: function () {
            console.log('Error al obtener los datos.');
        }
    });
}
function loadClientsByMonth() {
    $.ajax({
        url: '/Reports/ClientsByMonth',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            var propertyMonths = [];
            var propertyCount = [];
            
            $.each(data, function (index, item) {
                propertyMonths.push(item.Month);
                propertyCount.push(item.ClientCount);
            });

            var canvas = document.getElementById('barsClientsByMonth');
            var ctx = canvas.getContext('2d');

            // Destruir cualquier gráfico existente en el canvas
            if (canvas.chart) {
                canvas.chart.destroy();
            }

            // Crear el nuevo gráfico
            canvas.chart = new Chart(ctx, {
                type: 'bar',
                data: {
                    labels: propertyMonths,
                    datasets: [{
                        label: 'Cantidad de clientes',
                        data: propertyCount,
                        backgroundColor: '#0B6E4F',
                        borderColor: '#0B6E4F',
                        borderWidth: 1
                    }]
                },
                options: {
                    responsive: true,
                    maintainAspectRatio: false,
                    plugins: {
                        legend: {
                            position: 'top',
                        },
                        title: {
                            display: true,
                            text: 'Cantidad de clientes registrados por mes',
                            font: {
                                size: 20
                            }
                        }
                    }
                },
            });

            // Manejar el evento de redimensionamiento de la ventana
            window.addEventListener('resize', function () {
                canvas.chart.resize(); // Vuelve a renderizar el gráfico cuando cambia el tamaño de la ventana
            });
        },
        error: function () {
            console.log('Error al obtener los datos.');
        }
    });
}
function loadEmployeesByStatus(colors) {
    $.ajax({
        url: '/Reports/EmployeesByStatus',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            var employeeCount = [];
            var employeeStatus = [];
            var backgroundColors = [];

            backgroundColors = colors;

            $.each(data, function (index, item) {
                employeeCount.push(item.EmployeeCount);
                employeeStatus.push(item.Status);
            });

            var canvas = document.getElementById('pieEmployeesByStatus');
            var ctx = canvas.getContext('2d');

            // Destruir cualquier gráfico existente en el canvas
            if (canvas.chart) {
                canvas.chart.destroy();
            }

            // Crear el nuevo gráfico
            canvas.chart = new Chart(ctx, {
                type: 'pie',
                data: {
                    labels: employeeStatus,
                    datasets: [{
                        label: 'Cantidad',
                        data: employeeCount,
                        backgroundColor: backgroundColors,
                        borderColor: backgroundColors,
                        borderWidth: 1
                    }]
                },
                options: {
                    responsive: true,
                    maintainAspectRatio: false,
                    plugins: {
                        legend: {
                            position: 'top',
                        },
                        title: {
                            display: true,
                            text: 'Cantidad de empleados por estado',
                            font: {
                                size: 20
                            }
                        }
                    }
                },
            });

            // Manejar el evento de redimensionamiento de la ventana
            window.addEventListener('resize', function () {
                canvas.chart.resize(); // Vuelve a renderizar el gráfico cuando cambia el tamaño de la ventana
            });
        },
        error: function () {
            console.log('Error al obtener los datos.');
        }
    });
}
function loadEmployeesByRole(colors) {
    $.ajax({
        url: '/Reports/EmployeesByRole',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            var employeeCount = [];
            var employeeRoles = [];
            var backgroundColors = [];

            backgroundColors = colors;

            $.each(data, function (index, item) {
                employeeCount.push(item.EmployeeCount);
                employeeRoles.push(item.Roles);
            });

            var canvas = document.getElementById('pieEmployeesByRole');
            var ctx = canvas.getContext('2d');

            // Destruir cualquier gráfico existente en el canvas
            if (canvas.chart) {
                canvas.chart.destroy();
            }

            // Crear el nuevo gráfico
            canvas.chart = new Chart(ctx, {
                type: 'pie',
                data: {
                    labels: employeeRoles,
                    datasets: [{
                        label: 'Cantidad',
                        data: employeeCount,
                        backgroundColor: backgroundColors,
                        borderColor: backgroundColors,
                        borderWidth: 1
                    }]
                },
                options: {
                    responsive: true,
                    maintainAspectRatio: false,
                    plugins: {
                        legend: {
                            position: 'top',
                        },
                        title: {
                            display: true,
                            text: 'Cantidad de empleados por rol',
                            font: {
                                size: 20
                            }
                        }
                    }
                },
            });

            // Manejar el evento de redimensionamiento de la ventana
            window.addEventListener('resize', function () {
                canvas.chart.resize(); // Vuelve a renderizar el gráfico cuando cambia el tamaño de la ventana
            });
        },
        error: function () {
            console.log('Error al obtener los datos.');
        }
    });
}
function loadQuotesByMonth() {
    $.ajax({
        url: '/Reports/QuotesByMonth',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            var quoteMonths = [];
            var quotesCount = [];
           
            $.each(data, function (index, item) {
                quoteMonths.push(item.Month);
                quotesCount.push(item.QuoteCount);
            });

            var canvas = document.getElementById('barsQuotationsByMonth');
            var ctx = canvas.getContext('2d');

            // Destruir cualquier gráfico existente en el canvas
            if (canvas.chart) {
                canvas.chart.destroy();
            }

            // Crear el nuevo gráfico
            canvas.chart = new Chart(ctx, {
                type: 'bar',
                data: {
                    labels: quoteMonths,
                    datasets: [{
                        label: 'Cantidad de cotizaciones',
                        data: quotesCount,
                        backgroundColor: '#0B6E4F',
                        borderColor: '#0B6E4F',
                        borderWidth: 1
                    }]
                },
                options: {
                    responsive: true,
                    maintainAspectRatio: false,
                    plugins: {
                        legend: {
                            position: 'top',
                        },
                        title: {
                            display: true,
                            text: 'Cantidad de cotizaciones registradas por mes para el año actual',
                            font: {
                                size: 20
                            }
                        }
                    }
                },
            });

            // Manejar el evento de redimensionamiento de la ventana
            window.addEventListener('resize', function () {
                canvas.chart.resize(); // Vuelve a renderizar el gráfico cuando cambia el tamaño de la ventana
            });
        },
        error: function () {
            console.log('Error al obtener los datos.');
        }
    });
}
function loadQuotesByStatus(colors) {
    $.ajax({
        url: '/Reports/QuotesByStatus',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            var quoteCount = [];
            var quoteStatus = [];
            var backgroundColors = [];

            backgroundColors = colors;

            $.each(data, function (index, item) {
                quoteCount.push(item.QuoteCount);
                quoteStatus.push(item.Status);
            });

            var canvas = document.getElementById('pieQuotesByStatus');
            var ctx = canvas.getContext('2d');

            // Destruir cualquier gráfico existente en el canvas
            if (canvas.chart) {
                canvas.chart.destroy();
            }

            // Crear el nuevo gráfico
            canvas.chart = new Chart(ctx, {
                type: 'pie',
                data: {
                    labels: quoteStatus,
                    datasets: [{
                        label: 'Estados',
                        data: quoteCount,
                        backgroundColor: backgroundColors,
                        borderColor: backgroundColors,
                        borderWidth: 1
                    }]
                },
                options: {
                    responsive: true,
                    maintainAspectRatio: false,
                    plugins: {
                        legend: {
                            position: 'top',
                        },
                        title: {
                            display: true,
                            text: 'Cantidad de cotizaciones por estado',
                            font: {
                                size: 20
                            }
                        }
                    }
                },
            });

            // Manejar el evento de redimensionamiento de la ventana
            window.addEventListener('resize', function () {
                canvas.chart.resize(); // Vuelve a renderizar el gráfico cuando cambia el tamaño de la ventana
            });
        },
        error: function () {
            console.log('Error al obtener los datos.');
        }
    });
}
function loadPendingAppoitmentsByEmployee() {
    $.ajax({
        url: '/Reports/PendingAppoitmentsByEmployee',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            var appointmentEmployees = [];
            var appointmentCount = [];
            var backgroundColors = [];

            for (var i = 0; i < data.length; i++) {
                backgroundColors.push(getRandomColor());
            }

            $.each(data, function (index, item) {
                appointmentEmployees.push(item.Employee);
                appointmentCount.push(item.AppointmentCount);
            });

            var canvas = document.getElementById('barsPendingAppointmentsByEmployee');
            var ctx = canvas.getContext('2d');

            // Destruir cualquier gráfico existente en el canvas
            if (canvas.chart) {
                canvas.chart.destroy();
            }

            // Crear el nuevo gráfico
            canvas.chart = new Chart(ctx, {
                type: 'bar',
                data: {
                    labels: appointmentEmployees,
                    datasets: [{
                        label: 'Cantidad de citas',
                        data: appointmentCount,
                        backgroundColor: '#0B6E4F',
                        borderColor: '#0B6E4F',
                        borderWidth: 1
                    }]
                },
                options: {
                    responsive: true,
                    maintainAspectRatio: false,
                    plugins: {
                        legend: {
                            position: 'top',
                        },
                        title: {
                            display: true,
                            text: 'Cantidad de citas asignadas por empleado',
                            font: {
                                size: 20
                            }
                        }
                    }
                },
            });

            // Manejar el evento de redimensionamiento de la ventana
            window.addEventListener('resize', function () {
                canvas.chart.resize(); // Vuelve a renderizar el gráfico cuando cambia el tamaño de la ventana
            });
        },
        error: function () {
            console.log('Error al obtener los datos.');
        }
    });
}
function loadAppointmentsByProperty() {
    $.ajax({
        url: '/Reports/AppointmentsByProperty',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            var propertyTitles = [];
            var propertyAppointments = [];
            
            $.each(data, function (index, item) {
                propertyTitles.push(item.Title);
                propertyAppointments.push(item.AppointmentCount);
            });

            var canvas = document.getElementById('appointmentsByProperty');
            var ctx = canvas.getContext('2d');

            // Destruir cualquier gráfico existente en el canvas
            if (canvas.chart) {
                canvas.chart.destroy();
            }

            // Crear el nuevo gráfico
            canvas.chart = new Chart(ctx, {
                type: 'bar',
                data: {
                    labels: propertyTitles,
                    datasets: [{
                        label: 'Cantidad de citas',
                        data: propertyAppointments,
                        backgroundColor: '#0B6E4F',
                        borderColor: '#0B6E4F',
                        borderWidth: 1
                    }]
                },
                options: {
                    responsive: true,
                    maintainAspectRatio: false,
                    plugins: {
                        legend: {
                            position: 'top',
                        },
                        title: {
                            display: true,
                            text: 'Cantidad de citas por propiedad',
                            font: {
                                size: 20
                            }
                        }
                    }
                },
            });

            // Manejar el evento de redimensionamiento de la ventana
            window.addEventListener('resize', function () {
                canvas.chart.resize(); // Vuelve a renderizar el gráfico cuando cambia el tamaño de la ventana
            });
        },
        error: function () {
            console.log('Error al obtener los datos.');
        }
    });
}
function loadAppointmentsByCreationAndAttention() {
    $.ajax({
        url: '/Reports/AppointmentsByCreationAndAttention',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            var months = [];
            var createdAppointments = [];
            var attendedAppointments = [];
            var backgroundColors = [];

            for (var i = 0; i < data.length; i++) {
                backgroundColors.push(getRandomColor());
            }

            $.each(data, function (index, item) {
                months.push(item.Month);
                createdAppointments.push(item.MonthCreateds);
                attendedAppointments.push(item.MonthAttendeds);
            });

            var canvas = document.getElementById('appointmentsByCreationAndAttention');
            var ctx = canvas.getContext('2d');

            // Destruir cualquier gráfico existente en el canvas
            if (canvas.chart) {
                canvas.chart.destroy();
            }

            // Crear el nuevo gráfico
            canvas.chart = new Chart(ctx, {
                type: 'bar',
                data: {
                    labels: months,
                    datasets: [{
                        label: 'Cantidad creadas',
                        data: createdAppointments,
                        backgroundColor: '#0B6E4F',
                        borderColor: '#0B6E4F',
                        borderWidth: 1
                    },
                    {
                        label: 'Cantidad atendidas',
                        data: attendedAppointments,
                        backgroundColor: '#28AFB0',
                        borderColor: '#28AFB0',
                        borderWidth: 1
                    }]
                },
                options: {
                    responsive: true,
                    maintainAspectRatio: false,
                    plugins: {
                        legend: {
                            position: 'top',
                        },
                        title: {
                            display: true,
                            text: 'Citas creadas y atendidas por mes del año actual',
                            font: {
                                size: 20
                            }
                        }
                    }
                },
            });

            // Manejar el evento de redimensionamiento de la ventana
            window.addEventListener('resize', function () {
                canvas.chart.resize(); // Vuelve a renderizar el gráfico cuando cambia el tamaño de la ventana
            });
        },
        error: function () {
            console.log('Error al obtener los datos.');
        }
    });
}
function loadAppointmentsByStatus(colors) {
    $.ajax({
        url: '/Reports/AppointmentsByStatus',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            var appointmentCount = [];
            var appointmentStatus = [];
            var backgroundColors = [];

            backgroundColors = colors;

            $.each(data, function (index, item) {
                appointmentCount.push(item.AppointmentCount);
                appointmentStatus.push(item.Status);
            });

            var canvas = document.getElementById('pieAppointmentsByStatus');
            var ctx = canvas.getContext('2d');

            // Destruir cualquier gráfico existente en el canvas
            if (canvas.chart) {
                canvas.chart.destroy();
            }

            // Crear el nuevo gráfico
            canvas.chart = new Chart(ctx, {
                type: 'pie',
                data: {
                    labels: appointmentStatus,
                    datasets: [{
                        label: 'Estados',
                        data: appointmentCount,
                        backgroundColor: backgroundColors,
                        borderColor: backgroundColors,
                        borderWidth: 1
                    }]
                },
                options: {
                    responsive: true,
                    maintainAspectRatio: false,
                    plugins: {
                        legend: {
                            position: 'top',
                        },
                        title: {
                            display: true,
                            text: 'Cantidad de citas por estado',
                            font: {
                                size: 20
                            }
                        }
                    }
                },
            });

            // Manejar el evento de redimensionamiento de la ventana
            window.addEventListener('resize', function () {
                canvas.chart.resize(); // Vuelve a renderizar el gráfico cuando cambia el tamaño de la ventana
            });
        },
        error: function () {
            console.log('Error al obtener los datos.');
        }
    });
}