/**
 * FULL CALENDAR JQuery plugin, you have a calender global variable, can be accessed around the application
 */
var calendar;

window.addEventListener("load", function () {

    /* initialize the calendar
-----------------------------------------------------------------*/
    var calendarEl = document.getElementById('calendar');
    if (calendarEl != null) {
        calendar  = new FullCalendar.Calendar(calendarEl, {
            themeSystem: 'bootstrap',
            headerToolbar: {
                left: 'prev,next today',
                center: 'title',
                right: 'dayGridMonth,timeGridWeek,timeGridDay,listWeek'
            },

            eventSources: [
                // your event source
                {
                    url: '/Event/GetEvents', // use the `url` property
                    color: '#2c3e50', 
                    textColor: '#fff',
                    error: function () {
                        // Handle error
                        console.log('Error fetching events');
                    },
                    events: function (info, successCallback, failureCallback) {
                        // Perform any necessary preprocessing or modifications to the event data
                        // Then, call the successCallback with the modified event data
                        successCallback(modifiedEventData);
                    }
                }
            ],
            
            eventClick: function (info) {
                var eventId = info.event._def.publicId;
                events.load_edit_modal(eventId);
            }
        });

        calendar.render();

        calendar.on('dateClick', function (info) {
            //console.log('clicked on ' + info.dateStr);
        });
    }
});
