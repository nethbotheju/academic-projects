// This array is used to store products informations
const items = [
  {
    id: 0,
    title: "Wrist Bands",
    img: "img/hand-band.png",
    description: "Soft, comfortable Silicone Wristbands",
    price: 5.0,
  },
  {
    id: 1,
    title: "Water Bottle | 500ml",
    img: "img/bottle.png",
    description: "Eco-friendly 500ml reusable water bottle",
    price: 15.5,
  },
  {
    id: 2,
    title: "Key Chain",
    img: "img/keytag.png",
    description: "Durable Keychain for Everyday Use",
    price: 5.3,
  },
  {
    id: 3,
    title: "Pen",
    img: "img/pen.png",
    description: "Stylish refillable pen for everyday work",
    price: 5.0,
  },
  {
    id: 4,
    title: "Cap",
    img: "img/cap.png",
    description: "Durable Cap for Everyday Wear",
    price: 16.0,
  },
];

// This array is used to store items that are in the cart
let cartList = [];

// This for loop use to display products
for (let i = 0; i < items.length; i++) {
  let parentElement = document.getElementById("items");
  let newDiv = document.createElement("div");
  newDiv.classList.add("item");
  newDiv.innerHTML = `
          <img src="${items[i].img}">
          <h3>${items[i].title}</h3>
          <p>${items[i].description}</p>
          <label for="quantity">Quantity: </label>
          <input type="number" id="quantity${i}" name="quantity" min="1" value="1">
  
          <label for="color">Color: </label>
          <select id="color${i}">
              <option value="Black" selected>Black</option>
              <option value="Yellow">Yellow</option>
              <option value="Blue">Blue</option>
          </select>

          <h4>&#163;${items[i].price.toFixed(2)}</h4>

          <button onclick="addToCart(${
            items[i].id
          }, 'quantity${i}', 'color${i}', ${
    items[i].price
  })">Add To Card</button>
      `;
  parentElement.appendChild(newDiv);
}

// This variable is used to store total
let totalPrice = 0;

// This function is used reload the cart
function reloadCart() {
  let parentElement = document.getElementById("cart-items");

  parentElement.innerHTML = "";

  for (let i = 0; i < cartList.length; i++) {
    let newDiv = document.createElement("div");
    newDiv.classList.add("cart");
    let item = items.find((item) => item.id === cartList[i].id);
    newDiv.innerHTML = `
            <img src="${item.img}">
            <p><b>${item.title}</b><br/>
            ${cartList[i].color}&nbsp;&nbsp;&nbsp;&nbsp;
            ${cartList[i].quantity} x ${item.price.toFixed(2)}<br/>
            &#163;${(item.price * cartList[i].quantity).toFixed(2)}</p>
    
            <img src="img/delete.png" onclick="remove(${cartList[i].id})">
        `;

    parentElement.appendChild(newDiv);
  }
  storeData();
}

// This function is used to add a item to the cart
function addToCart(id, quantityId, colorId, price) {
  let quantity = document.getElementById(quantityId).value;
  let color = document.getElementById(colorId).value;

  let newItem = {
    id: id,
    quantity: quantity,
    color: color,
  };

  totalPrice += price * quantity;
  document.getElementById("total").innerHTML = totalPrice.toFixed(2);

  cartList.push(newItem);
  reloadCart();
}

// This function is used to remove a item from the cart
function remove(id) {
  for (let i = 0; i < cartList.length; i++) {
    if (cartList[i].id == id) {
      let item = items.find((item) => item.id === id);
      totalPrice -= item.price * cartList[i].quantity;
      if (totalPrice < 0.009) {
        totalPrice = 0;
      }
      document.getElementById("total").innerHTML = totalPrice.toFixed(2);

      cartList.splice(i, 1);
      break;
    }
  }
  reloadCart();
}

// This function is used to save cart data locally
function storeData() {
  localStorage.setItem("cartList", JSON.stringify(cartList));
  localStorage.setItem("totalPrice", totalPrice.toFixed(2));
  localStorage.setItem("items", JSON.stringify(items));
}

// This function checks whether the cart is empty or not
function checkoutBtn() {
  if (totalPrice == 0) {
    alert(
      "The cart is empty! Please add items to your cart before proceeding to checkout."
    );
  } else {
    location.href = "checkout.html";
  }
}

// Below functions are used for nav bar
function showSidebar() {
  const sidebar = document.querySelector(".sidebar");
  sidebar.style.display = "flex";
}
function hideSidebar() {
  const sidebar = document.querySelector(".sidebar");
  sidebar.style.display = "none";
}
