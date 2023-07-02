
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

    let data = JSON.parse(jsonData);
    let productData = [];

    for (const item of data) {
        await fetchJsonData(item.productId).then(product => {
            productData.push(product.data)
        })
    };

    showcart(productData, data);
}


// cart 
function showcart(productData, data) {
    console.log(data)
    // Check if data exists in local storage
    if (productData.length > 0) {
        // Get the container element to display the data
        let container = $('.data-container');
        container.empty();

        let total = $('.total-amount');
        total.empty();

        // Iterate over the data array

        console.log(productData)

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
                                    <p>${product.price}</p>
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
        let data = JSON.parse(jsonData);
        let totalAmount = 0;

        data.forEach(item => {
            totalAmount += item.totalAmount;
        });
        const totalAmountElement = document.getElementById('totalAmount');
        totalAmountElement.textContent = 'Total Amount: $' + totalAmount;
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

$(document).ready(function () {
    loadProducts();
    calculateTotalAmount();
});


// how can i make the POST request of the cart order?


function ConfirmShopping() {
    const url = 'https://example.com/api/cart/order';
    const data = {
        cartId: 123456,
        shippingAddress: {
            address1: '123 Main Street',
            address2: 'Apt. 123',
            city: 'Anytown',
            state: 'CA',
            zipCode: '12345'
        }
    };

    fetch(url, {
        method: 'POST',
        body: JSON.stringify(data)
    })
        .then(response => response.json())
        .then(data => console.log(data));

}