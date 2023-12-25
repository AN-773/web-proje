//Get Hospitals from /api/Filters/Hospitals
function getHospitals() {
    var hospitals = [];
    $.ajax({
        url: "/api/Filters/Hospitals",
        type: "GET",
        async: false,
        success: function (data) {
            hospitals = data;
        }
    });
    return hospitals;
}

//Get Specialities from /api/Filters/Specialities?hospitalId=1
function getSpecialities(hospitalId) {
    var specialities = [];
    $.ajax({
        url: "/api/Filters/Specialities?hospitalId=" + hospitalId,
        type: "GET",
        async: false,
        success: function (data) {
            specialities = data;
        }
    });
    return specialities;
}

//Get Clinics from /api/Filters/Clinics?hospitalId=1&specialityId=1
function getClinics(hospitalId, specialityId) {
    var clinics = [];
    $.ajax({
        url: "/api/Filters/Clinics?hospitalId=" + hospitalId + "&specialityId=" + specialityId,
        type: "GET",
        async: false,
        success: function (data) {
            clinics = data;
        }
    });
    return clinics;
}

//Get Doctors from /api/Filters/Doctors?hospitalId=1&specialityId=1&clinicId=1
function getDoctors(hospitalId, specialityId, clinicId) {
    var doctors = [];
    $.ajax({
        url: "/api/Filters/Doctors?hospitalId=" + hospitalId + "&specialityId=" + specialityId + "&clinicId=" + clinicId,
        type: "GET",
        async: false,
        success: function (data) {
            doctors = data;
        }
    });
    return doctors;
}

function getAllDoctors() {
    var doctors = [];
    $.ajax({
        url: "/api/Filters/AllDoctors",
        type: "GET",
        async: true,
        success: function (data) {
            $(document).trigger('doctorsLoaded', [data]);
        }
    });
}
$(document).ready(function () {
    $(document).on('translationLoaded', function () {

        var specialitiesFilter = $('#specialitiesFilter')
        var hospitalsFilter = $('#hospitalsFilter')
        var clinicsFilter = $('#clinicsFilter')
        var hospitals = getHospitals();
        hospitalsFilter.append(`<option value="0">${t('SelectHospital')}</option>`);
        $.each(hospitals, function (i, hospital) {
            $('#hospitalsFilter').append('<option value="' + hospital.id + '">' + hospital.name + '</option>');
        });
        hospitalsFilter.change(function () {
            var hospitalId = $(this).val();
            if (hospitalId == 0) {
                getAllDoctors();
                $('#specialitiesFilter').empty();
                $('#clinicsFilter').empty();
                return;
            }
            var specialities = getSpecialities(hospitalId);
            $('#clinicsFilter').empty();
            $('#specialitiesFilter').empty();
            $('#specialitiesFilter').append(`<option value="0">${t('SelectSpeciality')}</option>`);
            $.each(specialities, function (i, speciality) {
                $('#specialitiesFilter').append('<option value="' + speciality.id + '">' + t(speciality.name) + '</option>');
            });
        });
        specialitiesFilter.change(function () {
            var hospitalId = hospitalsFilter.val();
            var specialityId = specialitiesFilter.val();
            var clinics = getClinics(hospitalId, specialityId);
            $('#clinicsFilter').empty();
            $('#clinicsFilter').append(`<option value="0">${t('SelectClinic')}</option>`);
            $.each(clinics, function (i, clinic) {
                $('#clinicsFilter').append('<option value="' + clinic.id + '">' + t(clinic.name) + '</option>');
            });
        });
        clinicsFilter.change(function () {
            var hospitalId = hospitalsFilter.val();
            var specialityId = specialitiesFilter.val();
            var clinicId = clinicsFilter.val();
            var doctors = getDoctors(hospitalId, specialityId, clinicId);
            $(document).trigger('doctorsLoaded', [doctors]);
        });
        getAllDoctors();
    });
});
