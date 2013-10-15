def externalConceptReference1 = new ExternalConceptReferenceBuilder()
	.withEnvironmentId("623")
	.withIdspace("MED")
	.withId("5555");

def externalConceptReference2 = new ExternalConceptReferenceBuilder()
	.withEnvironmentId("623")
	.withIdspace("MED")
	.withId("5555");

def referenceBinding1 = new ReferenceBindingBuilder()
	.withExternalConceptReference(externalConceptReference1);

def referenceBinding2 = new ReferenceBindingBuilder()
	.withExternalConceptReference(externalConceptReference2);

def enumerationValueReferences1 = new EnumerationValueReferencesBuilder()
	.withReferenceBinding(referenceBinding1);

def dataElementReference1 = new DataElementReferenceBuilder()
	.withEnvironmentId("623")
	.withIdspace("MED")
	.withId("1232");

def dataElementReference2 = new DataElementReferenceBuilder()
	.withEnvironmentId("623")
	.withIdspace("MED")
	.withId("1232");

def dataElementReference3 = new DataElementReferenceBuilder()
	.withEnvironmentId("623")
	.withIdspace("MED")
	.withId("1232");

def dataElementReference4 = new DataElementReferenceBuilder()
	.withEnvironmentId("623")
	.withIdspace("MED")
	.withId("1232");

def dataElementReference5 = new DataElementReferenceBuilder()
	.withEnvironmentId("623")
	.withIdspace("MED")
	.withId("1231");

def enumerationValueReferences2 = new EnumerationValueReferencesBuilder()
	.withReferenceBinding(referenceBinding2);

def dataElementReference6 = new DataElementReferenceBuilder()
	.withEnvironmentId("623")
	.withIdspace("MED")
	.withId("1232");

def dataElementReference7 = new DataElementReferenceBuilder()
	.withEnvironmentId("623")
	.withIdspace("MED")
	.withId("1232");

def dataElementReference8 = new DataElementReferenceBuilder()
	.withEnvironmentId("623")
	.withIdspace("MED")
	.withId("1232");

def dataElementReference9 = new DataElementReferenceBuilder()
	.withEnvironmentId("623")
	.withIdspace("MED")
	.withId("1232");

def dataElementReference10 = new DataElementReferenceBuilder()
	.withEnvironmentId("623")
	.withIdspace("MED")
	.withId("1231");

def singleSelectDataElementInstance = new SingleSelectDataElementInstanceBuilder()
	.withDataElementReference(dataElementReference1)
	.withEnumerationValueReferences(enumerationValueReferences1);

def textDataElementInstance = new TextDataElementInstanceBuilder()
	.withDataElementReference(dataElementReference2)
	.withValue("As needed for pain");

def integerDataElementInstance = new IntegerDataElementInstanceBuilder()
	.withDataElementReference(dataElementReference3)
	.withValue("100");

def decimalDataElementInstance = new DecimalDataElementInstanceBuilder()
	.withDataElementReference(dataElementReference4)
	.withValue("100.0");

def booleanDataElementInstance = new BooleanDataElementInstanceBuilder()
	.withDataElementReference(dataElementReference5)
	.withValue("true");

def authoringConceptReference1 = new AuthoringConceptReferenceBuilder()
	.withId("12")
	.withDisplayName("Orderable")
	.withIsZynxTerm("true");

def singleSelectDataElementInstance = new SingleSelectDataElementInstanceBuilder()
	.withDataElementReference(dataElementReference6)
	.withEnumerationValueReferences(enumerationValueReferences2);

def textDataElementInstance = new TextDataElementInstanceBuilder()
	.withDataElementReference(dataElementReference7)
	.withValue("As needed for pain");

def integerDataElementInstance = new IntegerDataElementInstanceBuilder()
	.withDataElementReference(dataElementReference8)
	.withValue("100");

def decimalDataElementInstance = new DecimalDataElementInstanceBuilder()
	.withDataElementReference(dataElementReference9)
	.withValue("100.0");

def booleanDataElementInstance = new BooleanDataElementInstanceBuilder()
	.withDataElementReference(dataElementReference10)
	.withValue("true");

def authoringConceptReference2 = new AuthoringConceptReferenceBuilder()
	.withId("11")
	.withDisplayName("Medication")
	.withIsZynxTerm("false");

def externalConceptReference3 = new ExternalConceptReferenceBuilder()
	.withEnvironmentId("619")
	.withIdspace("client")
	.withId("Medication")
	.withSecondaryKey("Medication")
	.withUri("http://localhost/api/v1/catalog/env/619/orderables/client,Medication");

def dataElementInstances1 = new DataElementInstancesBuilder()
	.withDataElementInstance(booleanDataElementInstance)
	.withDataElementInstance(decimalDataElementInstance)
	.withDataElementInstance(integerDataElementInstance)
	.withDataElementInstance(textDataElementInstance)
	.withDataElementInstance(singleSelectDataElementInstance);

def dataTemplateReference1 = new DataTemplateReferenceBuilder()
	.withEnvironmentId("623")
	.withIdspace("MED")
	.withId("123");

def referenceBinding3 = new ReferenceBindingBuilder()
	.withAuthoringConceptReference(authoringConceptReference1);

def dataElementInstances2 = new DataElementInstancesBuilder()
	.withDataElementInstance(booleanDataElementInstance)
	.withDataElementInstance(decimalDataElementInstance)
	.withDataElementInstance(integerDataElementInstance)
	.withDataElementInstance(textDataElementInstance)
	.withDataElementInstance(singleSelectDataElementInstance);

def dataTemplateReference2 = new DataTemplateReferenceBuilder()
	.withEnvironmentId("623")
	.withIdspace("MED")
	.withId("123");

def referenceBinding4 = new ReferenceBindingBuilder()
	.withExternalConceptReference(externalConceptReference3)
	.withAuthoringConceptReference(authoringConceptReference2);

def orderDetail1 = new OrderDetailBuilder()
	.withDataTemplateReference(dataTemplateReference1)
	.withDataElementInstances(dataElementInstances1);

def orderableReferences1 = new OrderableReferencesBuilder()
	.withReferenceBinding(referenceBinding3);

def orderDetail2 = new OrderDetailBuilder()
	.withDataTemplateReference(dataTemplateReference2)
	.withDataElementInstances(dataElementInstances2);

def orderableReferences2 = new OrderableReferencesBuilder()
	.withReferenceBinding(referenceBinding4);

def orderableSentenceItem = new OrderableSentenceItemBuilder()
	.withOrderableReferences(orderableReferences1)
	.withRelatedToMedications("false")
	.withOrderDetail(orderDetail1);

def orderableSentenceItem = new OrderableSentenceItemBuilder()
	.withOrderableReferences(orderableReferences2)
	.withRelatedToMedications("true")
	.withOrderDetail(orderDetail2);

def items = new ItemsBuilder()
	.withItem(orderableSentenceItem)
	.withItem(orderableSentenceItem);

def orderSetSectionItem = new OrderSetSectionItemBuilder()
	.withItems(items);

def topLevelItems = new TopLevelItemsBuilder()
	.withTopLevelItem(orderSetSectionItem);

def orderSet = new OrderSetBuilder()
	.withTopLevelItems(topLevelItems);

