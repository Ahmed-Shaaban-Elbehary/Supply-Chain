window.addEventListener("load", function () {

    /* initialize the calendar
-----------------------------------------------------------------*/
    var calendarEl = document.getElementById('calendar');
    if (calendarEl != null) {
        var calendar = new FullCalendar.Calendar(calendarEl, {
            themeSystem: 'bootstrap',
            headerToolbar: {
                left: 'prev,next today',
                center: 'title',
                right: 'dayGridMonth,timeGridWeek,timeGridDay,listWeek'
            },
            editable: true,
            droppable: true, // this allows things to be dropped onto the calendar
            eventSources: [
                // your event source
                {
                    url: '/Event/GetEvents', // use the `url` property
                    color: '#2c3e50', 
                    textColor: '#fff'
                }
            ],
            eventClick: function (info) {
                var eventId = info.event._def.publicId;
                events.load_edit_modal(eventId);
            }
        });

        calendar.render();

        calendar.on('dateClick', function (info) {
            console.log('clicked on ' + info.dateStr);
        });
    }
});
