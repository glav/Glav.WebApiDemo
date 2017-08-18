/// <reference path="../_references.js" />

$(document).ready(function () {

	$("#help-action").bind("click", function () {
		var opts = {
			type: "GET",
			contentType: "application/json",
			dataType: "json",
			success: function (result) {
				$("#help-container").empty().fadeIn().html(result);
			}
		};
		$.ajax("/api/Explorer/", opts);

	});
});