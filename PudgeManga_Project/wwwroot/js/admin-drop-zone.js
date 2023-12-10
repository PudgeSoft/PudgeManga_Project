$(document).ready(function () {
    Dropzone.autoDiscover = false;

    // Simple Dropzonejs
    $("#dZUpload").dropzone({
        url: "hn_SimpeFileUploader.ashx",
        addRemoveLinks: true,
        autoProcessQueue: true
        parallelUploads: 1
        maxFilesize: 500
        init: function () {
            this.on("success", function (file, response) {
                var imgName = response;
                file.previewElement.classList.add("dz-success");
                console.log("Successfully uploaded: " + imgName);
            });

            this.on("error", function (file, response) {
                file.previewElement.classList.add("dz-error");
            });
        }
    });
});
