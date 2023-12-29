$(document).on('doctorsLoaded', function (obj, res) {
    var doctorsList = $('#doctorsList');
    console.log(res);
    doctorsList.empty();
    $.each(res, function (i, resObj) {
        doctorsList.append(
            ` <div class="col-md-4 mb-4">
                <div class="card">
                    <img width="200px" height="200px" src="uploads/${resObj.image}" class="card-img-top" style="object-fit: scale-down" alt="Doctor Image">
                    <div class="card-body">
                        <h5 class="card-title">${resObj.user}</h5>
                        <p class="card-text">${t('Speciality')}: ${t(resObj.speciality)}</p>
                        <p class="card-text">${t('Clinic')}: ${t(resObj.clinic)}</p>
                    </div>
                    <div class="card-footer">
                        <a class="btn btn-primary" href="/Home/MakeAppointment?doctorid=${resObj.doctor.id}">${t('MakeAnAppointment')}</a>
                    </div>
                </div>
            </div>`
        );
    });
});
