// import {draw_svg_text} from "./animations.js";

function append_url_params(args = "?foo='bar'") {
    console.log('hell0 from append_url_params()')
    window.history.replaceState(null, null, args);
}

function persist(key = 'foo', value = 'bar') {
    let preexisting_item = localStorage.getItem(key);
    if (!!preexisting_item) {
        localStorage.setItem(key, value);
        return value;
    }
    if (!preexisting_item) {
        localStorage.setItem(key, value);
        return value;
    }

    throw new Error("You really messed up if you got here.");
}


/** animations */
function draw_svg_text() {
    anime({
        targets: '.logo-path',
        strokeDashoffset: [anime.setDashoffset, 0],
        duration: 3000,
        endDelay: 1000,
        easing: 'linear',
        loop: true,
    });
}

window.append_url_params = append_url_params;
window.persist = persist;
window.draw_svg_text = draw_svg_text;
