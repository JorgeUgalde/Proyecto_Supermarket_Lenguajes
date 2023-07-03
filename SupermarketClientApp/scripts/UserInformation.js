

function SaveInformation() {
    // Obtener el formulario y registrar el evento de envío
    var form = document.getElementById('configForm');
    form.addEventListener('submit', function (event) {
        event.preventDefault();

        // Obtener los valores del formulario
        var name = document.getElementById('name').value;
        var phone = document.getElementById('phone').value;
        var email = document.getElementById('email').value;
        var address = document.getElementById('address').value;

        var userDataLS = JSON.parse(localStorage.getItem('userData'));
        if (userDataLS == null) {
            var userData = {
                UserIdentification : 0,
                Name: name,
                PhoneNumber: phone,
                Email: email,
                StreetAddress: address
            };
        }else {
            var userData = {
                UserIdentification : userDataLS.UserIdentification,
                Name: name,
                PhoneNumber: phone,
                Email: email,
                StreetAddress: address
            };
        }

        // Crear un objeto con los datos del usuario
       
        saveUserOnBD(JSON.stringify(userData));    
    });
}

$(document).ready(function () {

   verifyLS();
});

function verifyLS(){
    var userDataLS = JSON.parse(localStorage.getItem('userData'));
    if (userDataLS != null) {

        // fill the form with the local storage information
        document.getElementById('name').value = userDataLS.Name;
        document.getElementById('phone').value = userDataLS.PhoneNumber;
        document.getElementById('email').value = userDataLS.Email;
        document.getElementById('address').value = userDataLS.StreetAddress;    
    }
}

function saveUserOnBD(userData) {

console.log(userData);

    $.ajax({
        url: 'http://proyectoapps-001-site1.atempurl.com/Customer/Home/CreateUser',
        type: 'POST',
        contentType: 'application/json',
        data: userData,
        success: function (response) {
            alert("Added succesfully");
            user = JSON.parse(userData);
            user.UserIdentification = response.id;
            console.log(user);

            localStorage.setItem('userData', JSON.stringify(user));


           window.location.href = '../pages/Index.html';


        },
        error: function (error) {
            // Handle any errors that occur during the request
            console.error(error);
        }
    });
}