$(() => {
    //build the hub connection
    let connection = new signalR.HubConnectionBuilder().withUrl("/Notification").build()
    connection.start()
    connection.on("refreshEmployees", function () {
        loadData() //after database change, it will call this function to refresh the data.
    })
    loadData(); //when page load call the loadData method to display the data. 
    function loadData() {
        var tr = ''

        $.ajax({
            url: '/Employee/GetEmployees',
            method: 'GET',
            success: (result) => {
                $.each(result, (k, v) => {
                    tr = tr + `<tr>
                         <td>${v.id}</td>
                         <td>${v.name}</td>
                         <td>${v.age}</td>
                     </tr>`
                })

                $("#tableBody").html(tr)
            },
            error: (error) => {
                console.log(error)
            }
        })
    }
}) 