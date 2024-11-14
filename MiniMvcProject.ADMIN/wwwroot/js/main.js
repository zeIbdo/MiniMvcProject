function markImageForDeletion(button) {
    const imageId = button.getAttribute('data-id');

    const imagesToDeleteField = document.getElementById('ImagesToDelete');

    let imagesToDelete = imagesToDeleteField.value ? imagesToDeleteField.value.split(',') : [];

    if (!imagesToDelete.includes(imageId)) {
        imagesToDelete.push(imageId);
        imagesToDeleteField.value = imagesToDelete.join(',');  
    }

    button.closest('div').style.display = 'none';
}