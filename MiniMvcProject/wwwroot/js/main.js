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