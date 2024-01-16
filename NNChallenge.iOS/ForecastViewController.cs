using CoreGraphics;
using Foundation;
using System;
using System.Collections.Generic;
using UIKit;

namespace NNChallenge.iOS
{
    public partial class ForecastViewController : UICollectionViewController
    {
        private static readonly NSString forecastCellId = new NSString("ForecastCell");
        private static readonly NSString headerId = new NSString("Header");
        private readonly List<string> listOfString;

        public ForecastViewController() : base("ForecastViewController", null)
        {
        }

        public ForecastViewController(UICollectionViewLayout layout) : base(layout)
        {
            listOfString = new List<string>();
            for (int i = 0; i < 20; i++)
            {
                listOfString.Add($"Test+{i}");
            }
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Line Layout
            var lineLayout = new LineLayout()
            {
                HeaderReferenceSize = new CGSize(160, 100),
                ScrollDirection = UICollectionViewScrollDirection.Vertical
            };

            CollectionView.RegisterClassForCell(typeof(ForecastCell), forecastCellId);
            CollectionView.RegisterClassForSupplementaryView(typeof(Header), UICollectionElementKindSection.Header, headerId);

            //UIMenuController.SharedMenuController.MenuItems = new UIMenuItem[] {
            //    new UIMenuItem ("Custom", new Selector ("custom"))
            //};
            Title = "Forecast";
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        public override nint NumberOfSections(UICollectionView collectionView)
        {
            return 1;
        }

        public override nint GetItemsCount(UICollectionView collectionView, nint section)
        {
            return listOfString.Count;
        }

        public override UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
        {
            var forecastCell = (ForecastCell)collectionView.DequeueReusableCell(forecastCellId, indexPath);

            var animal = listOfString[indexPath.Row];

            forecastCell.SetImage(new UIImage());

            return forecastCell;
        }

        public override UICollectionReusableView GetViewForSupplementaryElement(UICollectionView collectionView, NSString elementKind, NSIndexPath indexPath)
        {
            var headerView = (Header)collectionView.DequeueReusableSupplementaryView(elementKind, headerId, indexPath);
            headerView.Text = "Supplementary View";
            return headerView;
        }
    }

    public class ForecastCell : UICollectionViewCell
    {
        private readonly UIImageView imageView;

        [Export("initWithFrame:")]
        public ForecastCell(CGRect frame) : base(frame)
        {
            BackgroundView = new UIView { BackgroundColor = UIColor.Orange };

            SelectedBackgroundView = new UIView { BackgroundColor = UIColor.Green };

            ContentView.Layer.BorderColor = UIColor.LightGray.CGColor;
            ContentView.Layer.BorderWidth = 2.0f;
            ContentView.BackgroundColor = UIColor.White;
            ContentView.Transform = CGAffineTransform.MakeScale(0.8f, 0.8f);

            imageView = new UIImageView(UIImage.FromBundle("placeholder.png"))
            {
                Center = ContentView.Center,
                Transform = CGAffineTransform.MakeScale(0.7f, 0.7f)
            };

            ContentView.AddSubview(imageView);
        }

        public void SetImage(UIImage value)
        {
            imageView.Image = value;
        }
    }

    public class Header : UICollectionReusableView
    {
        private readonly UILabel label;

        public string Text
        {
            get
            {
                return label.Text;
            }
            set
            {
                label.Text = value;
                SetNeedsDisplay();
            }
        }

        [Export("initWithFrame:")]
        public Header(CGRect frame) : base(frame)
        {
            label = new UILabel() { Frame = new CGRect(0, 0, 300, 50), BackgroundColor = UIColor.Yellow };
            AddSubview(label);
        }
    }
}

