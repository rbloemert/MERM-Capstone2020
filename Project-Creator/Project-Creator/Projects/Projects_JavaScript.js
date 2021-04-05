function previewProjectImage() {
    const preview = document.getElementById("project_image");
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