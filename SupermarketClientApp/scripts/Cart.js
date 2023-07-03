let productData = [];
let data = null;
let totalAmount = 0;

function fetchJsonData(id) {

    return $.ajax({
        url: 'http://proyectoapps-001-site1.atempurl.com/Customer/Home/Details/' + id,
        type: 'GET',
        error: function (error) {
            console.error(error);
        }
    });
}

async function loadProducts() {
    let jsonData = localStorage.getItem('cart');

    if (!jsonData) {
        return false;
    }

    data = JSON.parse(jsonData);
    //let productData = [];

    for (const item of data) {
        await fetchJsonData(item.productId).then(product => {
            productData.push(product.data)
        })
    };

    showcart(data);
}


// cart 
function showcart(data) {
    // Check if data exists in local storage
    if (productData.length > 0) {
        // Get the container element to display the data
        let container = $('.data-container');
        container.empty();

        let total = $('.total-amount');
        total.empty();

        // Iterate over the data array

        //genereta foreach for the productData array?
        var index = 0;

        for (const product of productData) {

            const shoppingCart = `
                <div class="card mb-3">
                    <div class="card-body">
                        <div class="d-flex justify-content-between">
                            <div class="d-flex flex-row align-items-center">
                                <div>
                                <img
                                src="${product.pictureUrl}"
                                class="img-fluid rounded-3" alt="Shopping item" style="width: 65px;">
                                </div>
                                <div class="ms-3">
                                    <h5>${product.name}</h5>
                                </div>
                            </div>
                            <div class="d-flex flex-row align-items-center">
                                <div style="width: 50px;">
                                    <p>${data[index].quantity}</p>
                                </div>
                                <div style="width: 80px;">
                                    <p>${product.price.toLocaleString("es-CR", { style: "currency", currency: "CRC" })}</p>
                                </div>
                                <button type="button" onclick="removeItem(${product.id})" class="btn btn-danger btn-sm">X</button>
                            </div>
                        </div>  
                    </div>
                </div>
            `;
            container.append(shoppingCart);
            total.append('<p>Total Amount: ' + totalAmount + '</p>');
            index++;

        };
    } else {
        // Display a message if no data is found in local storage
        $('.data-container').html('<p>No items int the cart</p>');
    }
}

function calculateTotalAmount() {
    let jsonData = localStorage.getItem('cart');

    if (jsonData) {
        // Parse JSON data into an array
        // let data = JSON.parse(jsonData);
        
        data.forEach(item => {
            totalAmount += item.totalAmount;
        });
        const totalAmountElement = document.getElementById('totalAmount');
        totalAmountElement.textContent = 'Total Amount: ₡' + totalAmount;
    }
}

//remove item from cart from the product id
function removeItem(itemId) {
    // Retrieve JSON data from local storage
    let jsonData = localStorage.getItem('cart');

    // Check if data exists in local storage
    if (jsonData) {
        // Parse JSON data into an array
        let data = JSON.parse(jsonData);

        // Filter out the item that matches the given ID
        let updatedData = data.filter(item => item.productId !== itemId);

        // Update the JSON data in local storage
        localStorage.setItem('cart', JSON.stringify(updatedData));
    }
    location.reload();
}

function updateItem(itemId, quantity, index) {
    // Retrieve JSON data from local storage
    let jsonData = localStorage.getItem('cart');

    if (jsonData) {
        // Parse JSON data into an array
        let data = JSON.parse(jsonData);

        // Filter out the item that matches the given ID
        let updatedData = data.filter(item => item.productId !== itemId);
        console.log(updatedData);
        updatedData.push({ productId: itemId, quantity: quantity, totalAmount: quantity * productData[index].price });
        console.log(updatedData);

        // Update the JSON data in local storage
        localStorage.setItem('cart', JSON.stringify(updatedData));
    }
    location.reload();
}


$(document).ready(function () {
    loadProducts();
    calculateTotalAmount();
});


function confirmPurchase() {    
    var userDataLS = JSON.parse(localStorage.getItem('userData'));
    if (userDataLS == null) {
        alert("Before confirm your order, please register yourself in the aplication ")
        window.location.href = '../pages/UserInformation.html';

    }

    var index = 0;

    //Validate stock of products in productData array and update the stock in the database if the purchase is confirmed
    for (const product of productData) {
        if (product.inStock === 0) {
            alert("Error. There is no stock for the product " + product.name + "\nYour product has been removed from the cart");
            removeItem(product.id);
            calculateTotalAmount();
            //location.reload();
            return false;
        } else if (product.inStock < data[index].quantity) {
            alert("Error. There is not enough stock for the product " + product.name + "\nYour product has been updated to the available stock");
            updateItem(product.id, product.inStock, index);
            calculateTotalAmount();
            //location.reload();
            return false;
        } else if (product.isActive === 0) {
            alert("Error. The product " + product.name + " is not available in this moment" + "\nYour product has been removed from the cart");
            removeItem(product.id);
            calculateTotalAmount();
            //location.reload();
            return false;
        }
        index++;
    }

    const userData = JSON.parse(localStorage.getItem('userData'));

    const productsData = data.map(item => ({
        ProductId: item.productId,
        Quantity: item.quantity
    }));

    const payload = {
        ProductsData: productsData,
        UserId: userData.UserIdentification
    };

    console.log(data);
    $.ajax({
        url: 'http://proyectoapps-001-site1.atempurl.com/Customer/Home/CreateOrder',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(payload),
        success: function (response) {
            // Handle the response from the server if needed
            console.log(response);
        },
        error: function (error) {

            console.error(error);
        }
    });

    localStorage.removeItem('cart');
    alert("Your purchase has been confirmed");
    window.location.href = '../pages/Index.html';

}


function loadOrderData() {
    if (productData.length > 0) {
        let container = $('#confirm-order');
        container.empty();
        console.log("valido")

         var index = 0;
         let shoppingCart;

    for (const product of productData) {

        console.log(product.name)

        shoppingCart = `
            <div class="card mb-3">
                <div class="card-body">
                    <div class="d-flex justify-content-between">
                        <div class="d-flex flex-row align-items-center">                           
                            <div class="ms-3">
                                <h5>${product.name}</h5>
                            </div>
                        </div>
                        <div class="d-flex flex-row align-items-center">
                            <div style="width: 50px;">
                                <p>${data[index].quantity}</p>
                            </div>
                            <div style="width: 80px;">
                                <p>${product.price.toLocaleString("es-CR", { style: "currency", currency: "CRC" })}</p>
                            </div>
                            <button type="button" onclick="removeItem(${product.id})" class="btn btn-danger btn-sm">X</button>
                        </div>
                    </div>  
                </div>
            </div>
        `;
        container.append(shoppingCart);
        index++;
    }
    
    let amount = $('#Total-Amount');
    amount.append(`<p>Total Amount: ${totalAmount} </p>`);

    } else {
        $('#ConfirmBTN').prop('hidden' , true);

        $('#confirm-order').html('<p>No items int the cart</p>');
    }

}
