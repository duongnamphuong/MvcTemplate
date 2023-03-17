// Opentip
var tip1 = '<span style="color:#007bff;">Login button</br><b>Opentip example</b></span>';
new Opentip('#login-btn', tip1, { tipJoint: 'top', background: '#ff8400', borderColor: '#000000' });

// toggle to show/hide password - start
exampleInputPassword1 = document.getElementById("exampleInputPassword1");
hiddenCheckbox = document.getElementById("toggle-hidden-checkbox");
trigger = document.getElementById("toggle-trigger");
trigger.addEventListener('click', event => {
    hiddenCheckbox.checked = !hiddenCheckbox.checked;
    if (hiddenCheckbox.checked) {
        exampleInputPassword1.type = "text";
    } else {
        exampleInputPassword1.type = "password";
    }
});
// toggle to show/hide password - end
