using System;
using System.Collections.Generic;
using UIKit;
using Foundation;
using TestNativeApp.ViewModels;

namespace TestNativeApp.iOS
{
    public partial class ViewController : UIViewController
    {

        private RecordViewModel _recordViewModel;
        private AnalyzeViewModel _analyzeViewModel;

        private void PrepareView()
        {

            _recordViewModel = new RecordViewModel();
            _analyzeViewModel = new AnalyzeViewModel();

            AnalyzeButton.TouchUpInside += async (object sender, EventArgs e) => 
            {

                var analyzedInfoList = await _analyzeViewModel.AnalyzeTextAsync(VoiceTextField.Text);
                var attributedStringM = new NSMutableAttributedString(VoiceTextField.Text);

                foreach (var analyzedInfo in analyzedInfoList)
                {

                    if (analyzedInfo.Item2 > 0.3)
                        continue;

                    var voiceText = VoiceTextField.Text;
                    var foundIndex = voiceText.IndexOf(analyzedInfo.Item1, StringComparison.CurrentCultureIgnoreCase);
                    if (foundIndex == -1)
                        continue;

                    var analyzedAttributes = new UIStringAttributes();
                    analyzedAttributes.ForegroundColor = UIColor.Red;
                    analyzedAttributes.UnderlineStyle = NSUnderlineStyle.Thick;
                    attributedStringM.SetAttributes(analyzedAttributes, new NSRange(foundIndex,
                                                                                    analyzedInfo.Item1.Length));

                }

                var attributedString = new NSAttributedString(attributedStringM);
                AnalyzedLabel.AttributedText = attributedString;

            };

            var tapGesture = new UITapGestureRecognizer(async (UITapGestureRecognizer obj) => 
            {
                
                VoiceTextField.Text = string.Empty;
                VoiceTextField.Text = await _recordViewModel.RecordVoice();


            });
            VoiceImageView.AddGestureRecognizer(tapGesture);

        }

        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            PrepareView();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();


        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.        
        }
    }
}
