$(document).ready(function () {


});

function isEmpty(type, message) {

	if (message != null && message != '') {
		toastr[type](message);
	}
};

function SweetAlertDailogMessage(type, message, row,url) {
	Swal.fire({
		title: 'İşlem sonucu',
		text: message,
		type: type,
		showCancelButton: false,
		allowOutsideClick: false,
		confirmButtonColor: '#3085d6',
		cancelButtonColor: 'd33',
		confirmButtonText: 'Tamam'
	}).then(function(result){
		if (result.value) {
			if (row != null) {
				row.remove();
			}
			else {
				window.location = url;
			}
		}
	});
}

function ConvertToReferenceGroup(id) {
	if (id == 0) {
		return 'Resale';
	}
	if (id == 1) {
		return 'Non-Resale';
	}
}

$("#file").on("change", function () {

	var input = this;
	var url = $(this).val();

	var ext = url.substring(url.lastIndexOf('.') + 1).toLowerCase();

	if (input.files && input.files[0] && (ext == "xlsx" || ext == "xlsb" || ext == "xltm" || ext == "xls" || ext == "xml" || ext == "xlam" || ext == "xla" || ext == "xlw" || ext == "xlr")) {
		toastr["success"]("Excel Dosyası Başarıyla Eklendi İşleme Devam Edebilirsiniz");
		$("#fileButton").html('Dosya Seçili');
	}
	else {
		toastr["error"]("Lütfen Excel dosyası ekleyiniz");
		$(this).val("");
	}

});

function getFormattedDate(date) {

	var year = date.getFullYear();
	var month = (1 + date.getMonth()).toString();
	month = month.length > 1 ? month : '0' + month;
	var day = date.getDate().toString();
	day = day.length > 1 ? day : '0' + day;
	return month + '/' + day + '/' + year;
}

