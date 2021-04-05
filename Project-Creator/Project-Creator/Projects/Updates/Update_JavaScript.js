function txtDesc_TextChanged() {
    val = document.getElementById("txtDesc").value;
    document.getElementById("lblDescCounter").innerHTML = val.length + " of 255";
}

function previewTimelineImage() {
    const preview = document.getElementById("timeline_image");
    const file = document.getElementById('ImageUploader').files[0];
    const reader = new FileReader();

    reader.addEventListener("load", function () {
        // convert image file to base64 string
        preview.src = reader.result;
    }, false);

    if (file) {
        reader.readAsDataURL(file);
    }
}

const downloadToFile = (content, filename, contentType) => {
    const a = document.createElement('a');
    const file = new Blob([content], { type: contentType });

    a.href = URL.createObjectURL(file);
    a.download = filename;
    a.click();

    URL.revokeObjectURL(a.href);
};

function previewFile() {
    var preview = null;
    const file = document.getElementById('ContentUploader').files[0];
    const reader = new FileReader();

    reader.addEventListener("load", function () {
        // convert image file to base64 string
        preview.src = reader.result;
    }, false);

    if (file) {
        document.getElementById("FileImage").style.display = "none";
        document.getElementById("FileVideo").style.display = "none";
        document.getElementById("FilePDF").style.display = "none";
        document.getElementById("FileText").style.display = "none";
        document.getElementById("FileZip").style.display = "none";
        var complex = false;
        switch (file.type) {
            case ("image/jpeg"):
            case ("image/png"):
            case ("image/bmp"):
                document.getElementById("FileImage").style.display = "block";
                preview = document.getElementById("image_upload");
                break;
            case ("application/pdf"):
                document.getElementById("FilePDF").style.display = "block";
                preview = document.getElementById("viewer");
                reader.readAsDataURL(file);
                document.getElementById("wrapper").data = reader.result;
                complex = true;
                break;
            case ("video/mp4"):
                document.getElementById("FileVideo").style.display = "block";
                preview = document.getElementById("video_upload");
                break;
            case ("text/plain"):
                document.getElementById("FileText").style.display = "block";
                preview = document.getElementById("PDF_upload");
                break;
            case ("application/x-zip-compressed"):
                document.getElementById("FileZip").style.display = "block";
                preview = document.createElement("BUTTON");
                reader.readAsDataURL(file);
                document.querySelector('#zip_download').removeEventListener("click", btn_listener);
                document.querySelector('#zip_download').addEventListener('click', btn_listener = () => {
                    downloadToFile(reader.result, 'upload.zip', 'application/x-zip-compressed');
                });
                complex = true;
                break;
        }

        if (complex == false) {
            reader.readAsDataURL(file);
        }

    }
}