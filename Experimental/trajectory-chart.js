// https://gist.github.com/DazzGranto/8d53b062dd17fa63992ea13bb44e9728
const range = (start, stop, step = 1) =>
  Array.from({ length: (stop - start) / step + 1 }, (_, i) => start + i * step);

const labels2 = range((start = 100), (stop = 500), (step = 100));

const data = {
  labels: labels2,
  // From: https://www.thefirearmblog.com/blog/2018/07/19/love-300-blackout/
  datasets: [
    {
      label: "Hornady 190 gr Sub-X190 gr Sub-X 300 Blackout",
      data: [-1.5, 0, -33.4, -104.6, -217.3, -374],
      fill: false,
      borderColor: "rgb(75, 192, 192)",
      tension: 0.1,
    },
    {
      label: "300 BLACKOUT 125 GR HP AMERICAN GUNNER",
      data: [-1.5, 3.9, 0, -15.9],
      fill: false,
      borderColor: "rgb(0, 75, 192)",
      tension: 0.1,
    },
  ],
};

const config = {
  type: "line",
  data: data,
};

var trajectory = document.getElementById("trajectoryChart").getContext("2d");
var chart = new Chart(trajectory, config);
