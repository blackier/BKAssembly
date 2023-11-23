using Microsoft.UI.Composition;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Hosting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKAssembly.WinUI;

public class BKAnimation
{
    // https://learn.microsoft.com/zh-cn/windows/apps/design/motion/timing-and-easing
    public static Visual AddImplicitVector3KeyAnimation(UIElement element, string target, TimeSpan duration)
    {
        Visual vs = ElementCompositionPreview.GetElementVisual(element);
        Compositor compositor = vs.Compositor;


        var animation = compositor.CreateVector3KeyFrameAnimation();
        animation.InsertExpressionKeyFrame(0f, "this.StartingValue");
        animation.InsertExpressionKeyFrame(1f, "this.FinalValue");
        animation.Duration = duration;
        animation.Target = target;

        if (vs.ImplicitAnimations == null)
            vs.ImplicitAnimations = compositor.CreateImplicitAnimationCollection();

        vs.ImplicitAnimations.Add(target, animation);
        return vs;
    }

    public static Visual AddImplicitScalarKeyAnimation(UIElement element, string target, TimeSpan duration)
    {
        Visual vs = ElementCompositionPreview.GetElementVisual(element);
        Compositor compositor = vs.Compositor;


        var animation = compositor.CreateScalarKeyFrameAnimation();
        animation.InsertExpressionKeyFrame(0f, "this.StartingValue");
        animation.InsertExpressionKeyFrame(1f, "this.FinalValue");
        animation.Duration = duration;
        animation.Target = target;

        if (vs.ImplicitAnimations == null)
            vs.ImplicitAnimations = compositor.CreateImplicitAnimationCollection();

        vs.ImplicitAnimations.Add(target, animation);
        return vs;
    }
}
