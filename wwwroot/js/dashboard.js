google.charts.load('current', {
    packages: ['corechart']
});

google.charts.setOnLoadCallback(inicializarDashboard);

function inicializarDashboard() {

    desenharGraficoColunas(
        "grafico30Dias",
        "Vendas por Produto - Últimos 30 dias",
        vendas30Dias
    );

    desenharGraficoColunas(
        "grafico360Dias",
        "Vendas por Produto - Últimos 360 dias",
        vendas360Dias
    );

    desenharGraficoPizza(
        "graficoProdutosMaisVendidos",
        "Produtos Mais Vendidos",
        produtosMaisVendidos
    );

    desenharGraficoFaturamento(
        "graficoFaturamento",
        "Faturamento por Período",
        faturamentoMensal
    );
}

function desenharGraficoColunas(idElemento, titulo, dados) {

    var data = new google.visualization.DataTable();

    data.addColumn('string', 'Produto');
    data.addColumn('number', 'Quantidade');

    dados.forEach(function (item) {

        data.addRow([
            item.Descricao,
            item.Quantidade
        ]);

    });

    var options = {

        title: titulo,

        legend: {
            position: 'none'
        },

        height: 420

    };

    var chart = new google.visualization.ColumnChart(
        document.getElementById(idElemento));

    chart.draw(data, options);

}

function desenharGraficoPizza(idElemento, titulo, dados) {

    var data = new google.visualization.DataTable();

    data.addColumn('string', 'Produto');
    data.addColumn('number', 'Quantidade');

    dados.forEach(function (item) {

        data.addRow([
            item.Descricao,
            item.Quantidade
        ]);

    });

    var options = {

        title: titulo,

        pieHole: 0.4,

        chartArea: {
            width: '90%',
            height: '80%'
        },

        height: 450,

        legend: {
            position: 'right'
        }

    };

    var chart = new google.visualization.PieChart(
        document.getElementById(idElemento));

    chart.draw(data, options);

}

function desenharGraficoFaturamento(idElemento, titulo, dados) {

    var data = new google.visualization.DataTable();

    data.addColumn('string', 'Período');
    data.addColumn('number', 'Faturamento');

    dados.forEach(function (item) {

        var partes = item.Descricao.split('/');

        var mes = parseInt(partes[0]);
        var ano = partes[1];

        var nomesMeses = [
            '',
            'Jan',
            'Fev',
            'Mar',
            'Abr',
            'Mai',
            'Jun',
            'Jul',
            'Ago',
            'Set',
            'Out',
            'Nov',
            'Dez'
        ];

        var periodo = nomesMeses[mes] + '/' + ano;

        data.addRow([
            periodo,
            Number(item.ValorTotal)
        ]);

    });

    // Formatação dos valores para moeda brasileira
    var formatter = new google.visualization.NumberFormat({
        prefix: 'R$ ',
        decimalSymbol: ',',
        groupingSymbol: '.',
        fractionDigits: 2
    });

    formatter.format(data, 1);

    var options = {

        height: 450,

        curveType: 'function',

        lineWidth: 3,

        pointSize: 5,

        legend: {
            position: 'bottom'
        },

        chartArea: {
            width: '85%',
            height: '70%'
        },

        hAxis: {
            title: 'Período'
        },

        vAxis: {
            title: 'Faturamento',
            format: 'R$ #,##0.00'
        },

        tooltip: {
            textStyle: {
                fontSize: 13
            }
        }

    };

    var chart = new google.visualization.LineChart(
        document.getElementById(idElemento)
    );

    chart.draw(data, options);
}