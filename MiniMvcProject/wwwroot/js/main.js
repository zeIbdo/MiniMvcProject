async function fetchProducts(categoryId) {
    try {
        const response = await fetch(`/Home/RelatedProducts?categoryId=${categoryId}`);
        const html = await response.text();

        document.querySelector('.product-slider').innerHTML = html;

    } catch (error) {
        console.error('Error fetching products:', error);
    }
}
async function addToBasket(id) {
    try {
        const response = await fetch(`/Home/AddToBasket?productId=${id}`);
        const html = await response.text();

        const cartDropdown = document.querySelector('.cart-block');
        cartDropdown.innerHTML = "";
        cartDropdown.innerHTML = html;
    }
    catch (error) {
        console.error('Error fetching products:', error);

    }
}
document.addEventListener("DOMContentLoaded", async function () {
    try {
        const response = await fetch('/Home/GetBasket');
        if (!response.ok) throw new Error('Failed to load basket');

        const html = await response.text();
        const cartDropdown = document.querySelector('.cart-block');
        const cartCount = document.querySelector('#basketCount');
        const cartPrice = document.querySelector('#basketPrice');
        cartDropdown.innerHTML = html;
    } catch (error) {
        console.error('Error loading basket:', error);
    }
});
//document.addEventListener('DOMContentLoaded', () => {
//    const updateQuantity = (input, delta) => {
//        const currentValue = parseInt(input.value) || 1;
//        const newValue = Math.max(1, currentValue + delta); 
//        input.value = newValue;


//        console.log('New Quantity:', newValue);
//    };

//    document.querySelectorAll('.minus-btn').forEach(button => {
//        button.addEventListener('click', (event) => {
//            const input = event.target.closest('td').querySelector('.quantity-input');
//            updateQuantity(input, -1);
//        });
//    });

//    // Handle plus button clicks
//    document.querySelectorAll('.plus-btn').forEach(button => {
//        button.addEventListener('click', (event) => {
//            const input = event.target.closest('td').querySelector('.quantity-input');
//            updateQuantity(input, 1);
//        });
//    });
//});

document.addEventListener('DOMContentLoaded', function () {
    const quantityBlocks = document.querySelectorAll('.count-input-block');

    quantityBlocks.forEach(block => {
        const minusBtn = block.querySelector('.minus-btn');
        const plusBtn = block.querySelector('.plus-btn');
        const input = block.querySelector('.quantity-input');

        if (minusBtn && plusBtn && input) {
            minusBtn.addEventListener('click', async (e) => {
                e.preventDefault();
                await fetch(minusBtn.href)
                updateQuantity(input, -1)
            });
            plusBtn.addEventListener('click', async (e) => {
                e.preventDefault();
                await fetch(plusBtn)
                updateQuantity(input, 1)
            });
        }
    });

    function updateQuantity(input, change) {
        if (!input) return;

        let currentValue = parseInt(input.value) || 0;
        let newValue = currentValue + change;

        newValue = Math.max(newValue, 1);

        input.value = newValue;

        calculateTotal();
    }

    function calculateTotal() {
        let total = 0;

        const productRows = document.querySelectorAll('tbody tr');

        productRows.forEach(row => {
            const quantityInput = row.querySelector('.quantity-input');
            const priceElement = row.querySelector('.pro-price');

            if (quantityInput && priceElement) {
                const quantity = parseInt(quantityInput.value) || 0;
                const price = parseFloat(priceElement.getAttribute('data-price')) || 0;

                total += quantity * priceElement.innerHTML;

                console.log(`Row - Quantity: ${quantity}, Price: ${priceElement.innerHTML}, Subtotal: ${quantity * price}`);
            }
        });

        const totalElement = document.querySelector('.cart-summary-wrap h2 span.text-primary');
        if (totalElement) {
            totalElement.textContent = `$${total.toFixed(2)}`;
            console.log('Updated total to:', total.toFixed(2));
        } else {
            console.error('Total element not found');
        }
    }

    calculateTotal();

    const updateBtn = document.querySelector('.update-btn');
    if (updateBtn) {
        updateBtn.addEventListener('click', function (e) {
            e.preventDefault();
            calculateTotal();
        });
    }
});



async function search() {
    const searchInput = document.getElementById("searchInput").value; 
    const dropdown = document.getElementById("searchDropdown"); 

    if (searchInput.length < 3) {
        dropdown.innerHTML = "";
        dropdown.classList.remove("show");
        return;
    }

    try {
        const response = await fetch(`/Home/ProductsForSearch?query=${searchInput}`);
        if (response.ok) {
            const html = await response.text(); 
            dropdown.innerHTML = html; 
            dropdown.classList.add("show");
        } else {
            console.error("Failed to fetch products:", response.status);
        }
    } catch (error) {
        console.error("Error during search:", error);
    }
}