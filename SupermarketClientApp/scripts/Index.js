function fetchJsonData() {
    $.ajax({
        url: 'http://proyectoapps-001-site1.atempurl.com/Customer/Home/GetAll',
        type: 'GET',
        success: function(data) {
            generateCards(data);
        }
    });
    }

    function generateCards(data) {
        let cardsContainer = $('#cardsContainer');
        cardsContainer.empty();
        data.data.forEach(item => {
            const card = `
            <div class="card">
              <img src="${item.pictureUrl}" class="card-img-top" style="height:160px;width:286px" alt="Product Image">
              <div class="card-body">
                <h5 class="card-title">${item.name}</h5>
                <p class="card-text">Price: $${item.price.toFixed(2)}</p>
                <p class="card-text">In Stock: ${item.inStock}</p>
                <p class="card-text">Unit: ${item.unit}</p>
                <button class="btn btn-primary" onclick="showDetails(${item.id})">View More</button>
              </div>
            </div>
          `;
          cardsContainer.innerHTML += card;
        });
    }

    $( document ).ready(function() {
        fetchJsonData();
    });
