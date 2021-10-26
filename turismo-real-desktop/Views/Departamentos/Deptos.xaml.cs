using MahApps.Metro.Controls;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using turismo_real_business.DTOs;
using turismo_real_controller.Controllers.Departamento;
using turismo_real_desktop.GridEntities;

namespace turismo_real_desktop.Views.Administrador.Departamentos
{
    public partial class Deptos : MetroWindow
    {
        private DepartamentoController deptoController;
        private List<DepartamentoDTO> deptos;

        public Deptos()
        {
            InitializeComponent();
            FillDataGridDeptos();
            contadorDeptos.Content = $"Total departamentos: {deptos.Count}";
        }


        public void FillDataGridDeptos()
        {
            deptoController = new DepartamentoController();
            deptos = deptoController.GetDepartamentos(); // obtener departamentos
            
            ObservableCollection<DeptoGrid> deptosGrid = new ObservableCollection<DeptoGrid>();
            List<DeptoGrid> deptosGridList = ConvertToDeptoGrid(deptos);

            foreach(DeptoGrid depto in deptosGridList)
            {
                deptosGrid.Add(depto);
            }
            dataGridDeptos.ItemsSource = deptosGrid;
            dataGridDeptos.IsReadOnly = true;
        }

        public List<DeptoGrid> ConvertToDeptoGrid(List<DepartamentoDTO> deptos)
        {
            List<DeptoGrid> deptosGridList = new List<DeptoGrid>();
            foreach(DepartamentoDTO depto in deptos)
            {
                DeptoGrid deptoGrid = new DeptoGrid();
                deptoGrid.id = depto.id_departamento;
                deptoGrid.rol = depto.rol;
                deptoGrid.tipo = depto.tipo;
                deptoGrid.superficie = depto.superficie.ToString() + " m2";
                deptoGrid.valorDiario = depto.valorDiario.ToString("C", CultureInfo.CurrentCulture);
                deptoGrid.comuna = depto.direccion.comuna;
                deptoGrid.region = depto.direccion.region;
                deptoGrid.estado = depto.estado;
                deptosGridList.Add(deptoGrid);
            }
            return deptosGridList;
        }


    }
}
