document.addEventListener('DOMContentLoaded', function () {

    /* initialize the calendar
-----------------------------------------------------------------*/
    var calendarEl = document.getElementById('calendar');
    var calendar = new FullCalendar.Calendar(calendarEl, {
        themeSystem: 'bootstrap',
        headerToolbar: {
            left: 'prev,next today',
            center: 'title',
            right: 'dayGridMonth,timeGridWeek,timeGridDay,listWeek'
        },
        editable: true,
        droppable: true, // this allows things to be dropped onto the calendar
    });

    calendar.render();

    calendar.on('dateClick', function (info) {
        console.log('clicked on ' + info.dateStr);
    });
});
