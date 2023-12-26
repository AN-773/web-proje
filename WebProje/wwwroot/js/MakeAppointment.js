$(document).ready(function () {
    
    function onAppointmentClicked() {
        let searchParams = new URLSearchParams(window.location.search)
        let id = searchParams.get('doctorid');
        let date = $(this).data('date');
        $.ajax({
            url: `/api/Appointments/MakeAppointment`,
            data: {date: date, doctorId: id},
            type: 'POST',
            success: function (data) {
                alert("Appointment made successfully");
                window.location.href = "/";
            }
        });
    }
    
    $('#datePicker')[0].onchange = (() => {
        let searchParams = new URLSearchParams(window.location.search)
        let id = searchParams.get('doctorid');
        let date = $('#datePicker').val();
        let year = date.split('-')[0];
        let month = date.split('-')[1];
        let day = date.split('-')[2];
        $.ajax({
            url: `/api/Appointments/AvailableAppointments?doctorid=${id}&year=${year}&month=${month}&day=${day}`,
            data: {date: date},
            type: 'GET',
            success: function (data) {
                let container = $('#timesContainer');
                container.empty();
                $.each(data, function (i, resObj) {
                    let btn = $("<button class='btn btn-outline-primary mx-4 my-3'></button>")
                        .text(resObj.split("T")[1].substring(0, 5))
                        .click(onAppointmentClicked);
                    btn.data('date', resObj)
                    container.append(btn)
                });
                if (!data || data.length === 0) {
                    container.append(
                        `
                        <h4 class="text-center">${t('NoAvailableAppointments')}</h4>
                        `
                    )
                }
            }
        });
    });
});
