// export 
// import anime from "animejs";
function draw_svg_text() {
    console.log('hello from  draw_svg_text ()')
    anime({
        targets: '.logo-path',
        strokeDashoffset: [anime.setDashoffset, 0],
        duration: 3000,
        endDelay: 1000,
        easing: 'linear',
        loop: true,
    });
}

console.log('loaded hydro svg js file...');
// window.draw_svg_text = draw_svg_text;