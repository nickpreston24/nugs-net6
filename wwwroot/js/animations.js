export function draw_svg_text() {
    anime({
        targets: '.logo-path',
        strokeDashoffset: [anime.setDashoffset, 0],
        duration: 3000,
        endDelay: 1000,
        easing: 'linear',
        loop: true,
    });
}