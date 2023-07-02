// Función para agregar al carrito
function addToCart() {
    // Obtener el objeto de producto almacenado en el local storage
    const productData = JSON.parse(localStorage.getItem('productData'));

    var quantity = document.getElementById("quantity").value;
    var totalAmount = productData.price * quantity;

    var cart = localStorage.getItem("cart");
    if (cart == null) {
        // No hay carrito aún
        cart = [];
        cart.push({
            "product": productData,
            "quantity": quantity,
            "totalAmount": totalAmount
        });
    } else {
        // El carrito ya existe
        cart = JSON.parse(cart);
        var index = cart.findIndex(x => x.product.id == productData.id);
        if (index == -1) {
            // El producto no está en el carrito
            cart.push({
                "product": productData,
                "quantity": quantity,
                "totalAmount": totalAmount
            });
        } else {
            // El producto ya está en el carrito
            cart[index].quantity = quantity;
            cart[index].totalAmount = totalAmount;
        }
    }

    // Guardar el carrito actualizado en el Local Storage
    localStorage.setItem("cart", JSON.stringify(cart));
    alert(productData.name + " added to cart. " + quantity + " items");
}

function validateQuantity() {
    var quantityInput = document.getElementById("quantity");
    var quantity = parseInt(quantityInput.value);

    // Verificar si el valor es menor que el mínimo
    if (quantity < parseInt(quantityInput.min)) {
        quantity = parseInt(quantityInput.min);
        quantityInput.value = quantity;
    }

    // Verificar si el valor es mayor que el máximo
    if (quantity > parseInt(quantityInput.max)) {
        quantity = parseInt(quantityInput.max);
        quantityInput.value = quantity;
    }
}

$(document).ready(function () {
    // Obtener el objeto de producto almacenado en el local storage
    const productData = JSON.parse(localStorage.getItem('productData'));



    // Crear el código HTML utilizando los datos del producto
    // Crear el código HTML utilizando los datos del producto
    let productHTML = `
<div class="card container pt-4">
  <div class="card-header bg-primary text-light ml-0 row">
    <div class="col-12 col-md-6">
      <h1 class="text-white-50">${productData.name}</h1>
    </div>
    <div class="col-12 col-md-6 text-end pt-4">
      <span class="badge bg-warning pt-2" style="height:30px;">${productData.barCode}</span>
    </div>
  </div>
  <div class="card-body row container">
    <div class="container rounded p-2">
      <div class="row">
        <div class="col-8 col-lg-8">
          <div class="row pl-2">
            <h5 class="text-muted pb-2">Price: ${productData.price}</h5>
          </div>
          <div class="row pl-2">
            <p class="text-secondary">${productData.name}</p>
          </div>
        </div>
        <div class="col-12 col-lg-3 offset-lg-1 text-center">
          <img src="${productData.pictureUrl}" width="100%" class="rounded" />
        </div>
        <div class="col-12 py-2">
          Quantity : <input type="number" value="1" max="${productData.inStock}" min="1" id="quantity" onchange="validateQuantity()" />
        </div>
      </div>
    </div>
  </div>
  <div class="card-footer">
    <div class="row">
      <div class="col-12 col-md-6 pb-1">
        <a class="btn btn-success form-control" style="height:50px;" href="./Index.html">Back to Home</a>
      </div>
      <div class="col-12 col-md-6 pb-1">
        <button class="btn btn-primary" style="height:50px; width:100%;" onclick="addToCart()">Add to Cart</button>
      </div>
    </div>
  </div>
</div>
`;


    // Insertar el código HTML en el elemento #product-details
    $('#product-details').html(productHTML);

});



