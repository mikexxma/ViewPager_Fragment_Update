using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V4.View;
using Android.Support.V4.App;
using Android.Views;
using System.Collections.Generic;
using Android.Support.V4.App;
using Java.Lang;
using Android.Runtime;
using static Android.Support.V4.View.ViewPager;


namespace ViewPagerDemo
{
    [Activity(Label = "ViewPagerDemo", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : FragmentActivity
    {
        static MyFragmentAdapter mainAdapter;
       
        private List<Android.Support.V4.App.Fragment> fragments = new List<Android.Support.V4.App.Fragment>();
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            ViewPager viewPager = FindViewById<ViewPager>(Resource.Id.viewpager);
            mainAdapter = new MyFragmentAdapter(SupportFragmentManager);
            fragments.Add(MyFragment.NewInstance(new String("first fragment")));
            fragments.Add(MyFragment.NewInstance(new String("second fragment")));
            fragments.Add(MyFragment.NewInstance(new String("third fragment")));
            mainAdapter.setFragments(fragments);
            viewPager.Adapter = mainAdapter;
            viewPager.SetOnPageChangeListener(new PageChangeListen());            
        }

        class MyHandler : Handler
        {
            public override void HandleMessage(Message msg)
            {
                if (msg.What == 1)
                {
                    mainAdapter.NotifyDataSetChanged();
                }
            }
        }

        class PageChangeListen : Java.Lang.Object, IOnPageChangeListener
        {
            public void OnPageScrolled(int position, float positionOffset, int positionOffsetPixels)
            {

            }

            public void OnPageScrollStateChanged(int state)
            {

            }

            public void OnPageSelected(int position)
            {
                if (position == 1)
                {
                    Message msg = new Message();
                    msg.What = 1;
                    new MyHandler().SendMessage(msg);
                }
            }
        }
    }
    

    public interface Updateable
    {
         void update();
    }

    public class MyFragment : Android.Support.V4.App.Fragment, Updateable
    {

        TextView questionBox;
        public MyFragment() { }

        public static MyFragment NewInstance(String question)
        {
            MyFragment fragment = new MyFragment();
            return fragment;
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.MyFragmentLayout, container, false);
            questionBox = (TextView)view.FindViewById(Resource.Id.textView1);
            return view;
        }

        public void update()
        {
            questionBox.Text = "Mike Update";
        }
    }


    class MyFragmentAdapter : FragmentPagerAdapter
    {
        private List<Android.Support.V4.App.Fragment> fragments;
        private Java.Lang.String[] titles= { new String("one"), new String("two"), new String("three") };
        public MyFragmentAdapter(Android.Support.V4.App.FragmentManager fm): base(fm)
        {
            
        }

        public override int GetItemPosition(Object @object)
        {
            MyFragment f = (MyFragment)@object;
            System.Console.WriteLine(f.Id + "" + f.Resources + "" + f.Tag);
            if (f != null)
            {
                f.update();
            }
            return base.GetItemPosition(@object);
        }
        public void setFragments(List<Android.Support.V4.App.Fragment> fragments)
        {
           
            this.fragments = fragments;
        }
        public override int Count
        {
            get { return fragments.Count; }
        }

        public override Android.Support.V4.App.Fragment GetItem(int position)
        {
            return fragments[position];
        }

        public override ICharSequence GetPageTitleFormatted(int position)
        {
            return titles[position];
        }
    }
}

