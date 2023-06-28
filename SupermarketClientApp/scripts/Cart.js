function addRemove() {
    var cart = {
        items: [],
        addItem: function(item) {
            this.items.push(item);
        },
        removeItem: function(item) {
            var index = this.items.indexOf(item);
            if (index >= 0) {
                this.items.splice(index, 1);
            }
        }
    };
    return cart;
}

function showcart() {
    const shoppingCart = `
<div class="container py-5 h-100">
    <div class="row d-flex justify-content-center align-items-center h-100">
        <div class="col">
        <h1 class="text-center mb-5">Shopping cart</h1>
            <div class="card">
                <div class="card-body p-4">
                    <div class="row">
                        <div class="col-lg-7">
                            <h5 class="mb-3">
                                <a href="${"../pages/Index.html"}" class="text-body">
                                    <i class="fas fa-long-arrow-alt-left me-2"></i>Continue shopping
                                </a>
                            </h5>
                            <hr>
                            <div class="d-flex justify-content-between align-items-center mb-4">
                                <div>
                                    <p class="mb-1">Shopping cart</p>
                                    <p>items in the cart</p>
                                </div>
                                <div>
                                    <p class="mb-0">
                                        <span class="text-muted">Sort by:</span> <a href="#!" class="text-body">price <i class="fas fa-angle-down mt-1"></i></a>
                                    </p>
                                </div>
                            </div>
                            <div class="card mb-3">
                                <div class="card-body">
                                    <div class="d-flex justify-content-between">
                                        <div class="d-flex flex-row align-items-center">
                                            <div>
                                                <p>pictureUrl</p>
                                            </div>
                                            <div class="ms-3">
                                                <h5>name</h5>
                                                <p class="small mb-0">small description(optional)</p>
                                            </div>
                                        </div>
                                        <div class="d-flex flex-row align-items-center">
                                            <div style="width: 50px;">
                                                <p>quantity</p>
                                            </div>
                                            <div style="width: 80px;">
                                                <p>price</p>
                                            </div>
                                            <a href="#!" style="color: #cecece;"><i class="fas fa-trash-alt"></i></a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-5">
                            <div class="card bg-primary text-white rounded-3">
                                <div class="card-body">
                                    <div class="d-flex justify-content-between align-items-center mb-4">
                                        <h5 class="mb-0">Details</h5>
                                        <img src="../images/user.png" class="img-fluid rounded-3" style="width: 45px;" alt="Avatar">
                                    </div>
                                    <hr class="my-4">
                                    <div class="d-flex justify-content-between">
                                        <p class="mb-2">Subtotal</p>
                                        <p class="mb-2">totalAmount</p>
                                    </div>
                                    <button type="button" class="btn btn-info btn-block btn-lg">
                                        <div class="d-flex justify-content-between">
                                            <span>Checkout <i class="fas fa-long-arrow-alt-right ms-2"></i></span>
                                        </div>
                                    </button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
`;

// Add the HTML content to a target element
const cartContainer = document.getElementById('cartContainer');
cartContainer.innerHTML = shoppingCart;
}

$( document ).ready(function() {
    showcart();
});