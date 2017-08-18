/// <reference path="../_references.js" />

$(document).ready(function () {

	$("#call-media-formatter").bind("click", function () {
		var opts = {
			type: "POST",
			// Some dodgy custom format
			data: "N|test,A1|Joe,A2|Bodgy",
			contentType: "application/x-dodgy",
			success: function (result) {
				$("#dodgy-result").fadeIn().text(result);
			}
		};
		$.ajax("/api/CustomBinding/", opts);

	});

	$("#get-site-stats").bind("click", function () {
		var opts = {
			type: "GET",
			success: function (result) {
				$("#site-stats-container").empty().fadeIn().html(result);
			}
		};
		$.ajax("/api/CustomBinding/", opts);
	});


	// need a delete button to finish thisslide demo
});