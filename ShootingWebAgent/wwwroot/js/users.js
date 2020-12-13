function confirmedDelete() {
    confirmedDelete();
}

function deleteAccount(accountId) {
    showDeleteModal("Delete Account","Are you sure you want to delete this account?", accountId)
}

var App = (function () {

    let savedAccountId;
    
    connection = new signalR.HubConnectionBuilder().withUrl("/updateHub").build();
    connection.on("Refresh", function (){
        location.reload();
    });
    connection.start();
    
    showDeleteModal = function(title, message, accountId) {
        savedAccountId = accountId;
        $("#deleteModalTitle").text(title);
        $("#deleteModalBody").text(message);
        $("#deleteUserModal").modal('show');
    }

    confirmedDelete = function () {
        connection.invoke("DeleteUser", savedAccountId).catch(function (err) {
            return showError("Error","Can't establish connection to the server!");
        });
    }
}());