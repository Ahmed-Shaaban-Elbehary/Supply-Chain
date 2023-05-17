prcategory = prcategory || {}

prcategory.get = {
    selector: $('.pagination a'),

    table: function (event) {
        event.preventDefault();
        var page = $(this).data('page');
        $.ajax({
            url: '@Url.Action("Category")',
            data: { page: page },
            success: function (result) {
                console.log(result);
            }
        });
    }
}