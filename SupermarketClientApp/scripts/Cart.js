function showcart() {

    let jsonData = localStorage.getItem('cart');
  
    // Check if data exists in local storage
    if (jsonData) {
        // Parse JSON data into an array
        let data = JSON.parse(jsonData);

        // Get the container element to display the data
        let container = $('.data-container');
        container.empty();

        // Iterate over the data array
        data.forEach(item => {
             //Create HTML elements for each item in the array
            //  let itemContainer = $('<div class="item-container"></div>');
            //   itemContainer.append('<p>Name: ' + item.product.name + '</p>');
            //   itemContainer.append('<p>Price: ' + item.product.price + '</p>');
            //   itemContainer.append('<p>Quantity: ' + item.quantity + '</p>');
            //   itemContainer.append('<p>Total Amount: ' + item.totalAmount + '</p>');
            //   itemContainer.append('<button type="button" onclick="removeItem(' + item.product.id + ')" class="btn btn-danger btn-sm">Remove</button>');
            //   container.append(itemContainer);

            const shoppingCart = `
                <div class="card mb-3">
                    <div class="card-body">
                        <div class="d-flex justify-content-between">
                            <div class="d-flex flex-row align-items-center">
                                <div>
                                <img
                                src="${item.product.pictureUrl}"
                                class="img-fluid rounded-3" alt="Shopping item" style="width: 65px;">
                                </div>
                                <div class="ms-3">
                                    <h5>${item.product.name}</h5>
                                </div>
                            </div>
                            <div class="d-flex flex-row align-items-center">
                                <div style="width: 50px;">
                                    <p>${item.quantity}</p>
                                </div>
                                <div style="width: 80px;">
                                    <p>${item.product.price}</p>
                                </div>
                                <button type="button" onclick="removeItem(${item.product.id})" class="btn btn-danger btn-sm">X</button>
                            </div>
                        </div>
                    </div>
                </div>
            `;
            container.append(shoppingCart);
        });

    } else {
        // Display a message if no data is found in local storage
        $('.data-container').html('<p>No items int the cart</p>');
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
      let updatedData = data.filter(item => item.product.id !== itemId);
  
      // Update the JSON data in local storage
      localStorage.setItem('cart', JSON.stringify(updatedData));
    }
    location.reload();
}

$( document ).ready(function() {
    showcart()
});