function showError(errorTitle, errorMessage) {
    $("#errorModalTitle").text(errorTitle);
    $("#errorModalBody").text(errorMessage);
    $("#myModal").modal('show');
}