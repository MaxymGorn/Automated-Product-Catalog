using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WpfApp5.Commands;
using WpfApp5.Models;

namespace WpfApp5.ViewModel
{
    class AppVM
    {
        #region констуктор  
        public AppVM()
        {
            Categories = new ObservableCollection<CategoryVM>();
            Organisations = new ObservableCollection<OrganisationVM>();
            Products = new ObservableCollection<Product>();
            LoadData();
        }
        #endregion

        #region функции

        bool ContainsName(int name)
        {
            bool eee = false;
            foreach (var el in Products)
            {
                if (el.Name == $"Новой продукт {name}")
                {
                    eee = true;
                    break;
                }
            }
            return eee;
        }
        private void UpdateData()
        {
            var select1 = Products.Where(c => c.Id == 0).ToList();
            foreach (var el in select1)
            {
                database.Product.Add(new Models.Product() 
                {  
                    Name=el.Name,
                    CategoryId=el.Category,
                    Id=el.Id,
                    Cost=el.Cost,
                    Count=el.Count, 
                    OrganisationId=el.Organisation
                });;
            }
            database.SaveChanges();
            LoadData();
        }


        private void LoadData()
        {
            Categories.Clear();
            var categories = database.Categories.ToList();
            foreach (var el in categories)
            {
                Categories.Add(new CategoryVM(el));
            }
            Organisations.Clear();
            dynamic organisation = database.Organisation.ToList();
            foreach (var el in organisation)
            {
                Organisations.Add(new OrganisationVM(el));
            }
            LoadVM();

        }

        void LoadVM()
        {

            Products.Clear();
            dynamic vMs = database.Product.ToList();
            foreach (var el in vMs)
            {
                Products.Add(new Product(el));
            }
        }
        private void UpdateFilter()
        {

            try
            {
                LoadVM();
                dynamic category;
                if (SelectedCategory != null)
                {
                    category = Products.Where(c => c.Category == SelectedCategory.Id).ToList();
               
                    if (category.Count == 0)
                    {
                        throw new Exception();
                    }
                    Products.Clear();
                    foreach (var el in category)
                    {
                        Products.Add(el);
                    }
                }
            }
            catch (Exception)
            {
                Products.Clear();
            }
            finally
            {
                dynamic priority;
                try
                {
                    if (SelectedOrganisations != null)
                    {
                        priority = Products.Where(c => c.Organisation == SelectedOrganisations.Id).ToList();
                        if (priority.Count == 0)
                        {
                            throw new Exception();
                        }
                        Products.Clear();
                        foreach (var el in priority)
                        {
                            Products.Add(el);
                        }
                    }
                }
                catch (Exception)
                {
                    Products.Clear();
                }
        
            }


        }

        #endregion

        #region Переменние и свойства
        public ObservableCollection<CategoryVM> Categories { get; set; }
        public ObservableCollection<OrganisationVM> Organisations { get; set; }
        public ObservableCollection<Product> Products { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        private CategoryVM selectedCategory;
        private OrganisationVM selectedOrganisations;
        private Product selectedVm;
       
        private Database1Entities7 database = new Database1Entities7();
        private RelayCommand Filter;
        private RelayCommand addProduct;
        private RelayCommand deleteProduct;
        private RelayCommand saveProduct;



        public CategoryVM SelectedCategory
        {
            get => this.selectedCategory;
            set
            {
                this.selectedCategory = value;
                OnPropertyChanged("SelectedCategory");
            }

        }



        public OrganisationVM SelectedOrganisations
        {
            get => this.selectedOrganisations;
            set
            {
                this.selectedOrganisations = value;
                OnPropertyChanged("SelectedOrganisations");
            }

        }
        public Product SelectedVm
        {
            get => this.selectedVm;
            set
            {
                this.selectedVm = value;
                OnPropertyChanged("SelectedVm");
            }

        }
        #endregion

        #region команды добавление категорий
        public RelayCommand Filtration
        {
            get
            {
                return Filter ?? (Filter = new RelayCommand(obj =>
                {
                    UpdateFilter();
                }));
            }
        }

        public RelayCommand AddProduct
        {
            get
            {
                return addProduct ?? (addProduct = new RelayCommand(obj =>
                {
                    Models.Product prod = new Models.Product() { Cost=10, OrganisationId=1, Count=3,CategoryId=1, Id=0};
                  
                    int i = 0;
                    while(true)
                    {
                        i++;
                        if(!ContainsName(i))
                        {
                            break;
                        }
                    }
                    prod.Name = "Новой продукт " +i.ToString();
                    Products.Add(new Product(prod));
                    UpdateData();
                }));
            }
        }

        public RelayCommand DeleteProduct
        {
            get
            {
                return deleteProduct ?? (deleteProduct = new RelayCommand(obj =>
                {
                    try
                    {
                        var product = database.Product.Where(c => c.Name == SelectedVm.Name).FirstOrDefault();
                        if (product == null)
                        {
                            throw new Exception();
                        }
                        var category = database.Categories.Where(c => c.Id == product.CategoryId).ToList();     
                        database.Categories.RemoveRange(category);
                        database.Product.Remove(product);
                        database.SaveChanges();
                        LoadData();

                    }
                    catch (Exception)
                    {

                    }
                }));
            }
        }


        #endregion
    }
}
