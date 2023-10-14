const notification = (() => {

    const GetEventList = () => {
        let url = '/Event/GetEventsList';
        app.ajax_request(url, 'GET', 'json', null)
            .then((response) => {
                notification.count_notifications(response.eventCount);
                if (response.displayGreenLight) {
                    notification.display_dot();
                }
                else {
                    notification.hide_dot();
                }
                if (response.eventViewModels.length == 0) {
                    notification.clear_notification();
                } else {
                    $.each(response.eventViewModels, (index, val) => {
                        notification.design_notification(val);
                    });
                }
            })
            .catch((jqXHR, textStatus, errorThrown) => {
                console.error(jqXHR);
                console.error(errorThrown);
            });
    }

    const CountNotifications = (n) => {
        $("#notification-list-header").html(`${n} Notification(s)`);
    }

    const AppendListToNotificationDiv = (html) => {
        $('#notification-list').find('#notification-container').append(html);
    }

    const DesignNotificationInitialization = (model) => {
        return `<blockquote id="event-blockqoute" onclick="events.on_event_block_quote_click(${model.id})" 
                    class="blockquote ${model.backgroundColor}">
                    <p class="mb-0 text-md text-muted">${model.description}</p>
                    <footer class="blockquote-footer">
                    ${app.getDateTimeFormat(model.publishedIn)} 
                        <cite title="Source Title">
                        <i class="fas fa-calendar-check mr-2"></i> ${model.title}
                        </cite>
                    </footer>
                </blockquote>`;
    }

    const AddDot = () => {
        $("#notificationbadge").addClass('dot');
    }

    const DisplayDot = () => {
        $("#notificationbadge").addClass('dot');
    }

    const HideDot = () => {
        $("#notificationbadge").removeClass('dot');
    }

    const ClearNotificationList = () => {
        $('#notification-list').find('#notification-container')
            .append(`<p class="text-center pb-5 pt-5 text-bold"> 
                                There's No Notifications Yet!
                            <p>`);
        $('#notification-list').find('#notification-container').css('overflow-y', 'hidden');
        $('#notification-list').find('#see-all-notifications').css('display', 'none');
    }

    const RecallingNotification = () => {
        $('#notification-list').on('hidden.bs.dropdown', function (e) {
            $('#notification-list').find('#notification-container').empty();
            GetEventList();
        })
    }

    const NotificationListCloased = () => {
        $('#notification-list').on('hide.bs.dropdown', function () {
            notification.clear_notification();
            notification.get();
        })
    }

    return {
        get: () => {
            GetEventList();
        },
        recall: () => {
            RecallingNotification();
        },
        design_notification: (model) => {
            let html = DesignNotificationInitialization(model);
            AppendListToNotificationDiv(html);
        },
        count_notifications: (number) => {
            CountNotifications(number)
        },
        clear_notification: () => {
            ClearNotificationList();
        },
        add_dot: () => {
            AddDot();
        },
        display_dot: () => {
            DisplayDot();
        },
        hide_dot: () => {
            HideDot();
        },
        notification_list_collapse: () => {
            NotificationListCloased();
        }
    }
})();

//self-invoking
(() => {
    notification.notification_list_collapse();
})();