// Opentip
var tip1 = '<span style="color:#007bff;">Login button</br><b>Opentip example</b></span>';
new Opentip('#login-btn', tip1, { tipJoint: 'top', background: '#ff8400', borderColor: '#000000' });

// hold/release to show/hide password - start
exampleInputPassword1 = document.getElementById("exampleInputPassword1");
function hold(target) {
    exampleInputPassword1.type = "text";
}
function release(target) {
    exampleInputPassword1.type = "password";
}

trigger = document.getElementById("toggle-trigger");
trigger.addEventListener('mousedown', event => {
    hold(event.currentTarget);
});
trigger.addEventListener('touchstart', event => {
    hold(event.currentTarget);
});
trigger.addEventListener('mouseup', event => {
    release(event.currentTarget);
});
trigger.addEventListener('mouseleave', event => {
    release(event.currentTarget);
});
trigger.addEventListener('mouseout', event => {
    release(event.currentTarget);
});
trigger.addEventListener('touchend', event => {
    release(event.currentTarget);
});
trigger.addEventListener('touchcancel', event => {
    release(event.currentTarget);
});
trigger.addEventListener('keydown', event => {
    if (event.code === 'Space') {
        hold(event.currentTarget);
    }
});
trigger.addEventListener('keyup', event => {
    if (event.code === 'Space') {
        release(event.currentTarget);
    }
});
// hold/release to show/hide password - end