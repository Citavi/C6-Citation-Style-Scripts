//COT 027
using System.Linq;
using System.Collections.Generic;
using SwissAcademic.Citavi;
using SwissAcademic.Citavi.Metadata;
using SwissAcademic.Collections;
using SwissAcademic.Drawing;

namespace SwissAcademic.Citavi.Citations
{
	public class ComponentPartFilter
		:
		IComponentPartFilter
	{
		public IEnumerable<ITextUnit> GetTextUnits(ComponentPart componentPart, Template template, Citation citation, out bool handled)
		{
			handled = false;
			
			if (citation == null) return null;
			if (citation.Reference == null) return null;
			if (componentPart == null) return null;
			if (componentPart.Elements == null || componentPart.Elements.Count == 0) return null;


			PageRangeFieldElement pageRangeFieldElement = componentPart.Elements.OfType<PageRangeFieldElement>().FirstOrDefault();
			if (pageRangeFieldElement == null) return null;

			bool found = false;
			TextUnitCollection textUnits = pageRangeFieldElement.GetTextUnits(citation, template);
			foreach(ITextUnit textUnit in textUnits)
			{
				if (textUnit.Text.Contains("-"))
				{
					found = true;
					textUnit.Text = textUnit.Text.Replace("-", "–");
				}
			}
			
			if (found) componentPart.Elements.ReplaceItem(pageRangeFieldElement, textUnits.TextUnitsToLiteralElements(componentPart));
			return null;
		}
	}
}
