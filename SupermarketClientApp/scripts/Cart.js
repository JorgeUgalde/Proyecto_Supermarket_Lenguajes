function removeItem(index) {
    // Retrieve JSON data from local storage
    let jsonData = localStorage.getItem('cart');
  
    // Check if data exists in local storage
    if (jsonData) {
      // Parse JSON data into an array
      let data = JSON.parse(jsonData);
  
      // Remove the item at the specified index
      data.splice(index, 1);
  
      // Update the JSON data in local storage
      localStorage.setItem('cart', JSON.stringify(data));
    }
  }

function showcart() {
    // //read from local storage and parse the data into an object from the details page
    // var cart = JSON.parse(localStorage.getItem('cart'));
    // //get the cart container
    // var cartContainer = document.getElementById('cartContainer');
    // //clear the container
    // cartContainer.innerHTML = '';
    // //loop through the items in the cart using ajax
    // for (var i = 0; i < cart.items.length; i++) {
    //     //show the picture, name, price and quantity
    //     var item = cart.items[i];
    //     var itemContainer = document.createElement('div');
    //     itemContainer.className = 'itemContainer';
    //     var itemImage = document.createElement('img');
    //     itemImage.src = item.pictureUrl;
    //     itemImage.className = 'itemImage';
    //     var itemName = document.createElement('p');
    //     itemName.innerHTML = item.name;
    //     itemName.className = 'itemName';
    //     var itemPrice = document.createElement('p');
    //     itemPrice.innerHTML = item.price;
    //     itemPrice.className = 'itemPrice';
    //     var itemQuantity = document.createElement('p');
    //     itemQuantity.innerHTML = item.quantity;
    //     itemQuantity.className = 'itemQuantity';
    //     var itemTotal = document.createElement('p');
    //     itemTotal.innerHTML = item.price * item.quantity;
    //     itemTotal.className = 'itemTotal';
    //     var removeButton = document.createElement('button');
    //     removeButton.innerHTML = 'Remove';
    //     removeButton.className = 'removeButton';
    //     removeButton.onclick = function() {
    //         cart.removeItem(item);
    //         localStorage.setItem('cart', JSON.stringify(cart));
    //         showcart();
    //     }
    //     itemContainer.appendChild(itemImage);
    //     itemContainer.appendChild(itemName);
    //     itemContainer.appendChild(itemPrice);
    //     itemContainer.appendChild(itemQuantity);
    //     itemContainer.appendChild(itemTotal);
    //     itemContainer.appendChild(removeButton);
    //     cartContainer.appendChild(itemContainer);
    // }

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
            // Create HTML elements for each item in the array
            let itemContainer = $('<div class="item-container"></div>');
            // itemContainer.append('<p>Name: ' + item.product.name + '</p>');
            // itemContainer.append('<p>Price: ' + item.product.price + '</p>');
            // itemContainer.append('<p>Quantity: ' + item.quantity + '</p>');
            // itemContainer.append('<p>Total Amount: ' + item.totalAmount + '</p>');

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
                                <a href="${removeItem(item.product.id)}" class="text-body">
                                    <img src="${"../images/removeIcon.png"}" style="height:10px;width:10px" alt="" id="removeimg">
                                </a>
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

$( document ).ready(function() {
    showcart()
});