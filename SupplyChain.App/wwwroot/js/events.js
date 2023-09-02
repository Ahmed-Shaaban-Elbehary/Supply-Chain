var base_url = "/Event";
var events = (() => {
    OpenGeneralModal = () => {
        $('#general-modal-content').empty();
        $('#general-modal-content').load('/Event/AddEditEvent');
    }
    return {
        show_modal_init: () => {
            var button = document.getElementById('open-event-modal');
            if (button != null)
                button.addEventListener('click', OpenGeneralModal);
        }
    }
})();

//self-invoking
(function () {
    events.show_modal_init();
})();